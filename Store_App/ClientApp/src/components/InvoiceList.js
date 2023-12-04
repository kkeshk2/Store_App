import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Card, CardBody, CardSubtitle, Col, Row } from 'reactstrap'

export default function InvoiceList() {

    const [invoiceList, setInvoiceList] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchInvoiceList = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/invoice/accessinvoicelist`, { headers });
                if (response.status === 401) {
                    navigate("/unauthorized")
                    window.location.reload()
                } else if (response.status === 404) {
                    navigate("/not-found")
                    window.location.reload()
                } else if (response.status === 500) {
                    navigate("/server-error")
                    window.location.reload()
                }  
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
                                        <CardSubtitle tag="h4">Total ${invoice.Total}</CardSubtitle>
                                    </Col>
                                </Row>
                                <br />
                                <Row>
                                    <Col>
                                        <CardSubtitle>{invoice.Date.substring(0, 10)}</CardSubtitle>
                                    </Col>
                                    <Col style={{ textAlign: "right" }}>
                                        <CardSubtitle>{invoice.Date.substring(11, 19)}</CardSubtitle>
                                    </Col>
                                </Row>
                                <br />
                                <Row hidden={invoice.Size === 1}>
                                    <Col>
                                        <CardSubtitle>{invoice.Size} Items</CardSubtitle>
                                    </Col>
                                </Row>
                                <Row hidden={invoice.Size !== 1}>
                                    <Col>
                                        <CardSubtitle>{invoice.Size} Item</CardSubtitle>
                                    </Col>
                                </Row>
                                <br />
                                <Row>
                                    <Col>
                                        Payment Method:<br />Credit Card **** **** **** {invoice.CreditCard}
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