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
        <div>
            <h1>Cart for (AccountId: {userAccountId})</h1>
            {cart ? (
                <div>
                    <img src="/cartImage.png" alt="Cart Image" style={{ width: '20%', height: '20%' }} />
                    {cart.cartId && <p>AccountId: ${cart.cartId}</p>}
                    {cart.cartProducts && <p>CartProducts: {cart.cartProducts}</p>}
                </div>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );


}

export default Cart;