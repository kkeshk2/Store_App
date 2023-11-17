import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

function Cart() {
    const [cart, setCart] = useState(null);
    const { id: userAccountId } = useParams();
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(`api/cart/getonebasedonaccountid?userAccountId=${userAccountId}`);
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }

                const data = await response.json(); // Parse the response as JSON

                console.log("NETWORK GOOD");
                console.log(data);
                setCart(data); // Set the product state with the parsed data
            } catch (error) {
                console.error('Error fetching product:', error);
            }
        };

        fetchData();
    }, [userAccountId]);

    return (
        <div style={{ maxWidth: '800px', margin: 'auto' }}>
            <table style={{ width: '100%', marginTop: '20px', borderCollapse: 'collapse' }}>
                <tbody>
                    <tr>
                        <td colSpan={2}>
                            <div style={{ width: '100%', display: 'flex', justifyContent: 'center' }}>
                                <img
                                    src={"/cartImage.png" || "/emptyImage.jpeg"}
                                    alt="Cart Image"
                                    style={{ width: '10%', height: 'auto', display: 'block', margin: '0 auto' }}
                                />
                            </div>
                        </td>
                    </tr>
                    {cart && cart.Products && cart.Products.map(product => {
                        // Find the corresponding cart product to get the quantity
                        const cartProduct = cart.CartProducts.find(cartProd => cartProd.ProductId === product.ProductId);

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
                                        {/* Add more details as needed */}
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