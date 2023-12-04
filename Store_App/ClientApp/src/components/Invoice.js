import React, { useState, useEffect } from 'react';
import { Link, useNavigate, useParams, } from 'react-router-dom';
import { Card, CardBody, CardImg, CardSubtitle, Col, Row } from 'reactstrap'

export default function Invoice() {

    const { id: invoiceId } = useParams();
    const [invoice, setInvoice] = useState([]);
    const [billingAddress, setBillingAddress] = useState([]);
    const [shippingAddress, setShippingAddress] = useState([]);
    const [date, setDate] = useState("");
    const [time, setTime] = useState("");
    const [product, setProduct] = useState([]);
    const navigate = useNavigate();  

    useEffect(() => {
        const fetchInvoice = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/invoice/accessinvoice?invoiceId=${invoiceId}`, { headers });
                const data = await response.json();
                if (response.ok) {
                    setInvoice(data);
                    setBillingAddress(data.BillingAddress);
                    setShippingAddress(data.ShippingAddress);
                    setDate(data.Date.substring(0, 10));
                    setTime(data.Date.substring(11, 19));
                    setProduct(data.Products);
                } else if (response.status === 401) {
                    navigate("/unauthorized")
                    window.location.reload()
                } else if (response.status === 404) {
                    navigate("/not-found")
                    window.location.reload()
                } else if (response.status === 500) {
                    navigate("/server-error")
                    window.location.reload()
                }  
            } catch (error) {
                console.error('Error fetching data:', error);
                navigate("/")
                window.location.reload()
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
        fetchInvoice();       
    }, [invoiceId]);

    const renderInvoice = (invoice) => {
        return (
            <div>
                <div className="d-flex flex-wrap justify-content-center">
                    <Card style={{ margin: "10px", maxWidth: "40rem", width: "40rem" }}>
                        <CardBody>
                            <Row>
                                <Col>
                                    <h4>Invoice #{invoice.InvoiceId}</h4>
                                </Col>
                                <Col style={{ textAlign: "right" }}>
                                    <h4>Total: ${invoice.Total}</h4>
                                </Col>
                            </Row>
                            <br />
                            <Row>
                                <Col>
                                    {date}
                                </Col>
                                <Col style={{ textAlign: "right" }}>
                                    {time}
                                </Col>
                            </Row>
                            <br />
                            <Row>
                                <Col>
                                    Payment Method:<br />
                                    Credit Card **** **** **** {invoice.CreditCard}
                                </Col>
                            </Row>
                            <br />
                            <Row>
                                <Col>
                                    Billing Address:<br />
                                    {billingAddress.Name}<br />
                                    {billingAddress.Line1}<br />
                                    <div hidden={billingAddress.Line2 === null}>{billingAddress.Line2}</div>
                                    {billingAddress.City}, {billingAddress.State} {billingAddress.Postal}
                                </Col>
                            </Row>
                            <br />
                            <Row>
                                <Col>
                                    Shipping Address:<br />
                                    {shippingAddress.Name}<br />
                                    {shippingAddress.Line1}<br />
                                    <div hidden={shippingAddress.Line2 === null}>{shippingAddress.Line2}</div>
                                    {shippingAddress.City}, {shippingAddress.State} {shippingAddress.Postal}
                                </Col>
                            </Row>
                            <br />
                            <Row>
                                <Col>
                                    Tracking Number:<br />
                                    {invoice.TrackingNumber}
                                </Col>
                            </Row>
                        </CardBody>
                    </Card>
                </div>
                <div className="d-flex flex-wrap justify-content-center" style={{ gridColumnGap: "100%" }}>
                    {product.map((Item) => (
                        <Card style={{ margin: "10px", maxWidth: "40rem" }}>
                            <CardBody>
                                <Row>
                                    <Col xs="4">
                                        <Link to={`/product/${Item.Product.ProductId}`}>
                                        <CardImg
                                            src={Item.Product.ImageLocation}
                                            style={{ paddingBottom: "10px", paddingTop: "10px" }}
                                            href="/"
                                            />
                                        </Link>
                                    </Col>
                                    <Col xs="8">
                                        <div className="d-flex flex-wrap align-items-start">
                                            <CardSubtitle tag="h6">
                                                <Link to={`/product/${Item.Product.ProductId}`} style={{ color: "inherit", textDecoration: "none" }}>
                                                    {Item.Product.Name}
                                                </Link><br/><br/>
                                                {Item.Quantity} x ${Item.Price}
                                            </CardSubtitle>
                                        </div>
                                    </Col>
                                </Row>
                            </CardBody>
                        </Card>
                    ))}
                </div>
            </div>
        )
    }

    return (
        renderInvoice(invoice)
    )
}