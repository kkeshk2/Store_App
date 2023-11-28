import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import '../custom.css';

function Checkout() {
    const [totalPrice, setTotalPrice] = useState(null);
    const [formData, setFormData] = useState({
        name: '',
        creditCardNumber: '',
        shippingAddress: '',
    });
    const [loading, setLoading] = useState(true);


    useEffect(() => {
        const fetchData = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/cart/gettotalprice`, { headers });
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }

                const data = await response.json();
                setTotalPrice(data);
            } catch (error) {
                console.error('Error fetching total price:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [totalPrice]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value,
        });
    };

    if (loading) {
        return <p>Loading...</p>;
    }

    if (totalPrice === 0) {
        return (<div className="outer-container" style={{ paddingTop: '200px', textAlign: 'center' }}>
                    <h1>There is nothing in the cart to checkout</h1>
                </div >
        );
    }

    return (
        <div className="checkout-container">
            <h1 style={{ fontWeight: 'bold', textAlign: 'center' }}>Checkout</h1>
            <div className="total-price-checkout">
                {totalPrice !== null && (
                    <p>Total Price: ${totalPrice}</p>
                )}
            </div>
            <div className="form-container">
                <form className="form">
                    <div className="input-group">
                        <label htmlFor="name">Name:</label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            value={formData.name}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div className="input-group">
                        <label htmlFor="creditCardNumber">Credit Card Number:</label>
                        <input
                            type="text"
                            id="creditCardNumber"
                            name="creditCardNumber"
                            value={formData.creditCardNumber}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div className="input-group">
                        <label htmlFor="shippingAddress">Shipping Address:</label>
                        <textarea
                            id="shippingAddress"
                            name="shippingAddress"
                            value={formData.shippingAddress}
                            onChange={handleInputChange}
                            style={{ minHeight: '100px' }}
                            required
                        />
                    </div>

                    <Link to={"/payment-successful"}>
                        <button className="btn-pay">Pay Now</button>
                    </Link>
                </form>
            </div>
        </div>
    );
}

export default Checkout;
