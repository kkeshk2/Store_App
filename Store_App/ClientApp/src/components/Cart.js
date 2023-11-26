import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import '../custom.css';

function Cart() {
    const [cart, setCart] = useState(null);
    const [loading, setLoading] = useState(true);
    const { id: userAccountId } = useParams();
    const [refreshCart, setRefreshCart] = useState(false);
    useEffect(() => {
        const fetchData = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/cart/getonebasedonaccountid`, { headers });
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }

                const data = await response.json(); // Parse the response as JSON

                console.log("NETWORK GOOD");
                console.log(data);
                setCart(data); // Set the product state with the parsed data
            } catch (error) {
                console.error('Error fetching product:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [userAccountId, refreshCart]);

    const deleteFromCart = async (productId) => {
        // console.log("URL:", `api/cart/deletefromcart?accountId=${1}&productId=${productId}`);

        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/cart/deletefromcart?productId=${productId}`, { headers });
            // const response = await fetch(`api/cart/deletefromcart?accountId=${1}&productId=${productId}`);
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
        <div style={{ maxWidth: '800px', margin: 'auto' }}>
            <table style={{ width: '100%', marginTop: '20px', borderCollapse: 'collapse' }}>
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
                                // After deleting, you might want to refetch the cart data or update it in some way
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