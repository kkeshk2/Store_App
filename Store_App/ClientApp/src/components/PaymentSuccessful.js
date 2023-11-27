import React from 'react';

export const PaymentSuccessful = () => {
    handlePayment();
    return (
        <div className="outer-container" style={{ paddingTop: '200px', textAlign: 'center' }}>
            <h1>Order Has Been Placed. Thank You!</h1>
        </div >
    );
};

const handlePayment = async () => {

    try {
        const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
        const response = await fetch(`api/cart/deleteallfromcart`, { headers });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        console.log('NETWORK GOOD');
        console.log(data);

    } catch (error) {
        console.error('Error with Payment:', error);
    }
};
