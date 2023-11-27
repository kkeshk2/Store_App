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
        <div style={{ maxWidth: '800px', margin: 'auto', position: 'relative' }}>
            <h1 style={{ fontWeight: 'bold', textAlign: 'center' }}>Checkout</h1>
            <div
                style={{
                    position: 'absolute',
                    top: '56px',
                    right: '-300px',
                    backgroundColor: '#fff',
                    padding: '20px',
                    border: '1px solid #ccc',
                    borderRadius: '5px',
                    fontWeight: 'bold',
                    fontSize: '24px', // Increased font size for total price
                }}
            >
                {totalPrice !== null && (
                    <p>Total Price: ${totalPrice}</p>
                )}
            </div>
            <div
                style={{
                    backgroundColor: '#fff',
                    padding: '20px',
                    border: '1px solid #ccc',
                    borderRadius: '5px',
                    marginBottom: '20px',
                }}
            >
                <form style={{ display: 'flex', flexDirection: 'column' }}>
                    <label htmlFor="name" style={{ marginBottom: '8px' }}>Name:</label>
                    <input
                        type="text"
                        id="name"
                        name="name"
                        value={formData.name}
                        onChange={handleInputChange}
                        style={{ marginBottom: '15px', padding: '8px', borderRadius: '5px' }}
                        required
                    />
                    <label htmlFor="creditCardNumber" style={{ marginBottom: '8px' }}>Credit Card Number:</label>
                    <input
                        type="text"
                        id="creditCardNumber"
                        name="creditCardNumber"
                        value={formData.creditCardNumber}
                        onChange={handleInputChange}
                        style={{ marginBottom: '15px', padding: '8px', borderRadius: '5px' }}
                        required
                    />
                    <label htmlFor="shippingAddress" style={{ marginBottom: '8px' }}>Shipping Address:</label>
                    <textarea
                        id="shippingAddress"
                        name="shippingAddress"
                        value={formData.shippingAddress}
                        onChange={handleInputChange}
                        style={{ marginBottom: '15px', padding: '8px', borderRadius: '5px', minHeight: '100px' }}
                        required
                    />
                    <Link to={"/payment-successful"}>
                        < button className="btn-pay">Pay Now</button>
                    </Link>
                </form>
            </div>
        </div>
    );
}

export default Checkout;
