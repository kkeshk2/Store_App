import React, { useState, useEffect } from 'react';
import '../custom.css';

function Checkout() {
    const [totalPrice, setTotalPrice] = useState(null);
    const [refreshCheckout, setRefreshCheckout] = useState(false);
    const [formData, setFormData] = useState({
        name: '',
        creditCardNumber: '',
        shippingAddress: '',
    });

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
                console.error('Error fetching product:', error);
            }
        };

        fetchData();
    }, [totalPrice, refreshCheckout]);

    const handlePayment = async () => {
        console.log('Form Data:', formData);

        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/cart/deleteallfromcart`, { headers });
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log('NETWORK GOOD');
            console.log(data);
            alert('Order Successful');
            setTimeout(() => { setRefreshCheckout(prevState => !prevState); }, 3000);
            
        } catch (error) {
            console.error('Error with Payment:', error);
        }
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value,
        });
    };

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
                    <button onClick={handlePayment} className="btn-pay">
                        Pay Now
                    </button>
                </form>
            </div>
        </div>
    );
}

export default Checkout;
