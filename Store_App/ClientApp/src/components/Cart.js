import React, { useState, useEffect } from 'react';
import { Link, useParams, useNavigate } from 'react-router-dom';
import '../custom.css';
import { Button, ButtonGroup, Card, CardBody, CardImg, CardSubtitle, CardTitle, Col, Input, Row } from 'reactstrap'

function Cart() {
    const [cart, setCart] = useState([])
    const [total, setTotal] = useState(0)
    const navigate = useNavigate()

    useEffect(() => {
        const populateCart = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/cart/accesscart`, { headers });
                const data = await response.json();
                setCart(data.ProductList);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        const getTotal = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/cart/gettotal`, { headers });
                const data = await response.json();
                setTotal(data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        const verifyUser = async () => {
            if (localStorage.getItem("authtoken")) {
                try {
                    const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                    const response = await fetch(`api/account/verifyaccount`, { headers });
                    if (!response.ok) {
                        localStorage.removeItem("authtoken")
                    }
                } catch (Exception) {
                    localStorage.removeItem("authtoken")
                }
            }

            if (!localStorage.getItem("authtoken")) {
                navigate("/")
                window.location.reload()
            }
        }

        verifyUser();
        populateCart();
        getTotal();
    }, []);

    const updateCart = async (productId, quantity) => {
        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/cart/updatecart?productId=${productId}&quantity=${quantity}`, { headers });
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json();
            console.log('NETWORK GOOD');
            console.log(data);
        } catch (error) {
            console.error('Error adding to cart:', error);
        }
        console.log(`Cart updated.`);
        window.location.reload()
    };

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
        } catch (error) {
            console.error('Error adding to cart:', error);
        }
        console.log(`Item deleted.`);
        window.location.reload()
    };

    const clearCart = async () => {
        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/cart/clearcart`, { headers });
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json();
            console.log('NETWORK GOOD');
            console.log(data);
        } catch (error) {
            console.error('Error adding to cart:', error);
        }
        console.log(`Cart Cleared.`);
        window.location.reload()
    };

    const renderCart = (cart) => {
        return (
            <div>
                <div className="text-center" style={{ margin: "10px" }}>
                    <img
                        src="\cartImage.png"
                        style={{ width: 80, height: 80 }}
                    />
                </div>
                <div className="d-flex flex-wrap justify-content-center" style={{ gridColumnGap: "100%" }}>
                    {cart.map((Item) => (
                        <Card style={{ margin: "10px", maxWidth: "40rem" }}>
                            <CardBody>
                                <Row>
                                    <Col xs="4">
                                        <Link to={`/product/${Item.Product.ProductId}`}>
                                            <CardImg
                                                src={Item.Product.ProductImageLocation}
                                                style={{ paddingBottom: "10px", paddingTop: "10px" }}
                                                href="/"
                                            />
                                        </Link>
                                    </Col>
                                    <Col>
                                        <div className="d-flex align-items-start" style={{ height: "50%" }}>                                  
                                            <CardSubtitle tag="h6" hidden={Item.Product.ProductSale !== 0}>
                                                <Link to={`/product/${Item.Product.ProductId}`} style={{ color: "inherit", textDecoration: "none" }}>
                                                    {Item.Product.ProductName}
                                                </Link><br />
                                                ${Item.Product.ProductPrice}
                                            </CardSubtitle>
                                            <CardSubtitle tag="h6" hidden={Item.Product.ProductSale === 0}>
                                                <Link to={`/product/${Item.Product.ProductId}`} style={{ color: "inherit", textDecoration: "none" }}>
                                                    {Item.Product.ProductName}
                                                </Link><br />
                                                <s style={{ color: "darkred" }}>${Item.Product.ProductPrice}</s> ${Item.Product.ProductPrice - Item.Product.ProductSale}
                                            </CardSubtitle>
                                        </div>
                                        <div className="d-flex align-items-end" style={{ height: "50%" }}>
                                            <ButtonGroup>
                                                <Input type="select" size="sm" onChange={e => updateCart(Item.Product.ProductId, e.target.value)} >
                                                    <option value="" disabled selected hidden>{Item.Quantity}</option>
                                                    <option value="1">1</option>
                                                    <option value="2">2</option>
                                                    <option value="3">3</option>
                                                    <option value="4">4</option>
                                                    <option value="5">5</option>
                                                    <option value="6">6</option>
                                                    <option value="7">7</option>
                                                    <option value="8">8</option>
                                                    <option value="9">9</option>
                                                    <option value="10">10</option>
                                                </Input>
                                                <Button color="login" size="sm" type="" onClick={() => deleteFromCart(Item.Product.ProductId)}>
                                                    Delete
                                                </Button>
                                            </ButtonGroup>
                                        </div>
                                    </Col>
                                </Row>
                            </CardBody>
                        </Card>
                    ))}
                </div>
                <div className="d-flex flex-wrap justify-content-center">
                    <Card style={{ margin: "10px", maxWidth: "40rem", width: "40rem" }}>
                        <CardBody>
                            <Row>
                                <CardSubtitle tag="h4" style={{marginBottom: "10px"}}>
                                    Total: ${total.toFixed(2)}
                                </CardSubtitle>
                            </Row>
                            <Row>
                                <Col>
                                    <Button disabled={total === 0} block color="login">
                                        Checkout
                                    </Button>
                                </Col>
                                <Col>
                                    <Button disabled={total === 0} block color="login" onClick={() => clearCart()}>
                                        Clear
                                    </Button>
                                </Col>
                            </Row>
                        </CardBody>
                    </Card>
                </div>
            </div>
        )
    };

    return (
        renderCart(cart)
    )

}

export default Cart;