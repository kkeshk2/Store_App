import React, { Component, useState, useEffect } from 'react';
import { Link, useNavigate, useParams, redirect } from 'react-router-dom';
import { Button, ButtonGroup, Card, CardBody, CardImg, CardSubtitle, CardTitle, Col, Input, Row } from 'reactstrap'

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
                    setBillingAddress(data.InvoiceBillingAddress);
                    setShippingAddress(data.InvoiceShippingAddress);
                    setDate(data.InvoiceDate.substring(0, 10));
                    setTime(data.InvoiceDate.substring(11, 19));
                    setProduct(data.InvoiceProducts);
                } else {
                    navigate("/")
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
                                    <h4>Total: ${invoice.InvoiceTotal}</h4>
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
                                    Credit Card **** **** **** {invoice.InvoiceCreditCardLast4}
                                </Col>
                            </Row>
                            <br />
                            <Row>
                                <Col>
                                    Billing Address:<br />
                                    {billingAddress.Name}<br />
                                    {billingAddress.Line1}<br />
                                    <div hidden={billingAddress.Line2 === null}>{billingAddress.Line2}</div>
                                    {billingAddress.City}, {billingAddress.State} {billingAddress.PostalCode}
                                </Col>
                            </Row>
                            <br />
                            <Row>
                                <Col>
                                    Shipping Address:<br />
                                    {shippingAddress.Name}<br />
                                    {shippingAddress.Line1}<br />
                                    <div hidden={shippingAddress.Line2 === null}>{shippingAddress.Line2}</div>
                                    {shippingAddress.City}, {shippingAddress.State} {shippingAddress.PostalCode}
                                </Col>
                            </Row>
                            <br />
                            <Row>
                                <Col>
                                    Tracking Number:<br />
                                    {invoice.InvoiceTrackingNumber}
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
                                            src={Item.Product.ProductImageLocation}
                                            style={{ paddingBottom: "10px", paddingTop: "10px" }}
                                            href="/"
                                            />
                                        </Link>
                                    </Col>
                                    <Col xs="8">
                                        <div className="d-flex flex-wrap align-items-start">
                                            <CardSubtitle tag="h6">
                                                <Link to={`/product/${Item.Product.ProductId}`} style={{ color: "inherit", textDecoration: "none" }}>
                                                    {Item.Product.ProductName}
                                                </Link><br/><br/>
                                                {Item.Quantity} x ${Item.UnitPrice}
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