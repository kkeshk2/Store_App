import React, { Component, useState, useEffect } from 'react';
import { Link, useNavigate, useParams, redirect } from 'react-router-dom';
import { Button, ButtonGroup, Card, CardBody, CardImg, CardSubtitle, CardTitle, Col, Input, Row } from 'reactstrap'

export default function InvoiceList() {

    const [invoiceList, setInvoiceList] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchInvoiceList = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/invoice/accessinvoicelist`, { headers });
                const data = await response.json();
                setInvoiceList(data.Invoices)                                
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
        }

        if (localStorage.getItem("authtoken") == null) {
            navigate("/")
            window.location.reload()
        }

        verifyUser();
        fetchInvoiceList();
    }, []);


    const renderInvoiceList = (invoiceList) => {
        return (
            <div>
                <div className="d-flex flex-wrap justify-content-center">
                    <h4 className="text-center" style={{ margin: "10px" }}>Invoices</h4>
                </div>
                <div className="d-flex flex-wrap justify-content-center" style={{ gridColumnGap: "100%" }}>
                    {invoiceList.map((invoice) => (
                        
                        <Card style={{ margin: "10px", width: "40rem", maxWidth: "40rem" }}>
                            <Link to={`/invoice/${invoice.InvoiceId}`} style={{ color: "inherit", textDecoration: "none" }}>
                            <CardBody>
                                <Row>
                                    <Col>
                                        <CardSubtitle tag="h4">Invoice #{invoice.InvoiceId}</CardSubtitle>
                                    </Col>
                                    <Col style={{ textAlign: "right" }}>
                                        <CardSubtitle tag="h4">Total ${invoice.InvoiceTotal}</CardSubtitle>
                                    </Col>
                                </Row>
                                <br />
                                <Row>
                                    <Col>
                                        <CardSubtitle>{invoice.InvoiceDate.substring(0, 10)}</CardSubtitle>
                                    </Col>
                                    <Col style={{ textAlign: "right" }}>
                                        <CardSubtitle>{invoice.InvoiceDate.substring(11, 19)}</CardSubtitle>
                                    </Col>
                                </Row>
                                <br />
                                <Row hidden={invoice.InvoiceSize === 1}>
                                    <Col>
                                        <CardSubtitle>{invoice.InvoiceSize} Items</CardSubtitle>
                                    </Col>
                                </Row>
                                <Row hidden={invoice.InvoiceSize !== 1}>
                                    <Col>
                                        <CardSubtitle>{invoice.InvoiceSize} Item</CardSubtitle>
                                    </Col>
                                </Row>
                                <br />
                                <Row>
                                    <Col>
                                        Payment Method:<br />Credit Card **** **** **** {invoice.InvoiceCreditCardLast4}
                                    </Col>
                                </Row>
                                </CardBody>
                            </Link>
                            </Card>

                    ))}
                </div>
            </div>
        )
    }

    return (
        renderInvoiceList(invoiceList)
    )

}