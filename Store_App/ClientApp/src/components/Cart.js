import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import '../custom.css';

function Cart() {
    const [cart, setCart] = useState(null);
    const [totalPrice, setTotalPrice] = useState(null);
    const [loading, setLoading] = useState(true);
    const [refreshCart, setRefreshCart] = useState(false);
    useEffect(() => {
        const fetchData = async () => {

            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const [totalPriceResponse, cartResponse] = await Promise.all([
                    fetch(`api/cart/gettotalprice`, { headers }),
                    fetch(`api/cart/getonebasedonaccountid`, { headers })
                ]);

                if (!totalPriceResponse.ok) {
                    throw new Error('Total price network response was not ok');
                }
                const totalPriceData = await totalPriceResponse.json();
                setTotalPrice(totalPriceData);

                if (!cartResponse.ok) {
                    throw new Error('Cart network response was not ok');
                }
                const cartData = await cartResponse.json();
                setCart(cartData);
            } catch (error) {
                console.error('Error fetching data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [refreshCart, totalPrice]);

    const deleteFromCart = async (productId) => {

        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/cart/deletefromcart?productId=${productId}`, { headers });
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log('NETWORK GOOD');
            console.log(data);
            setRefreshCart(prevState => !prevState);
        } catch (error) {
            console.error('Error deleting from cart:', error);
        }
        console.log(`Product deleted from the cart`);
    }

    if (loading) {
        return <p>Loading...</p>;
    }

    if (!cart) {
        return <p>Error loading cart data</p>;
    }

    return (
        <div className="cart-container">
            <div className="cart-header">
                
                <div className="total-price-cart-container">
                    {totalPrice !== null && (
                        <p>Total Price: ${totalPrice}</p>
                    )}    
                    <Link to="/checkout">
                        <button className="btn-continue-to-payment">
                            Continue to Payment
                        </button>
                    </Link>
                </div>
            </div>
            
            <table className="cart-table">
                <tbody>
                    <tr>
                        <td colSpan={2}>
                            <div style={{ width: '100%', display: 'flex', justifyContent: 'center' }}>
                                <img
                                    src={"/cartImage.png" || "/emptyImage.jpeg"}
                                    alt="Cart Img"
                                    style={{ width: '10%', height: 'auto', display: 'block', margin: '0 auto' }}
                                />
                            </div>
                        </td>
                    </tr>
                    {cart && cart.Products && cart.Products.map(product => {
                        const cartProduct = cart.CartProducts.find(cartProd => cartProd.ProductId === product.ProductId);

                        const handleDelete = async () => {
                            if (cartProduct) {
                                await deleteFromCart(cartProduct.ProductId);
                            }
                        };

                        return (
                            <React.Fragment key={product.ProductId}>
                                <tr>
                                    <td colSpan={2}>
                                        <img
                                            src={product.ProductImageLocation || "/emptyImage.jpeg"}
                                            alt={product.ProductName}
                                            style={{ width: '20%', height: 'auto' }}
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td style={{ textAlign: 'center', padding: '10px', backgroundColor: '#f5f5f5' }} colSpan={2}>
                                        <h1>{product.ProductName}</h1>
                                        <p>Price: ${product.ProductPrice}</p>
                                        {cartProduct && <p>Quantity: {cartProduct.Quantity}</p>}
                                        {cartProduct && (
                                            <button
                                                className="btn-cart"
                                                onClick={handleDelete}
                                                style={{ marginTop: '10px' }}
                                            >
                                                Delete from Cart
                                            </button>
                                        )}
                                    </td>
                                </tr>
                            </React.Fragment>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
}

export default Cart;