import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import {
    Alert,
    Input,
    Label,
    Form,
    FormGroup,
    Row,
    Col,
    Button,
    Card,
    CardBody,
    CardSubtitle
} from 'reactstrap'

export default function Checkout() {
    const [creditCard, setCreditCard] = useState({})
    const [billingAddress, setBillingAddress] = useState({})
    const [shippingAddress, setShippingAddress] = useState({})
    const [validation, setValidation] = useState({})
    const [status, setStatus] = useState(0)
    const [total, setTotal] = useState(0)
    const navigate = useNavigate()

    const CreditCardRegex = /^[0-9]{16,16}$/
    const CreditCardVCRegex = /^[0-9]{3,4}$/
    const CreditCardExpMonthRegex = /^[1][012]$|^[0]?[1-9]$/
    const CreditCardExpYearRegex = /^[0-9]{4,4}$/
    const NameRegex = /^[A-Za-z][A-Za-z\.\-\x20]{0,127}$/
    const Line1Regex = /^[0-9][A-Za-z0-9\.\-\&\x20]{0,127}$/
    const Line2Regex = /^$|^[A-Za-z0-9][A-Za-z0-9\.\-\&\x20]{0,127}$/
    const CityRegex = /^[A-Za-z0-9][A-Za-z0-9\.\-\&\x20]{0,127}$/
    const StateRegex = /^[A-Z]{2,2}$/
    const PostalRegex = /^[0-9]{5,5}$/

    useEffect(() => {
        const getTotal = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/cart/accesscart`, { headers });
                if (response.status === 400) {
                    setStatus(400)
                } else if (response.status === 401) {
                    setStatus(401)
                    navigate('/unauthorized')
                    window.location.reload()
                } else if (response.status === 404) {
                    setStatus(404)
                    navigate('/not-found')
                    window.location.reload()
                } else if (response.status === 500) {
                    setStatus(500)
                }
                const data = await response.json();
                setTotal(data.Total);
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
        getTotal();
    }, [navigate]);

    const handleCreditCard = (field, value) => {
        setCreditCard({
            ...creditCard,
            [field]: value
        })
    }

    const handleBillingAddress = (field, value) => {
        setBillingAddress({
            ...billingAddress,
            [field]: value
        })
    }

    const handleShippingAddress = (field, value) => {
        setShippingAddress({
            ...shippingAddress,
            [field]: value
        })
    }

    const HandleSubmit = async event => {
        event.preventDefault();
        const creditCardString
            = creditCard["cardNumber"]
            + "\t" + creditCard["expirationMonth"]
            + "\t" + creditCard["expirationYear"]
            + "\t" + creditCard["verificationCode"]
        const billingAddressString
            = billingAddress["name"]
            + "\t" + billingAddress["line1"]
            + "\t" + billingAddress["line2"]
            + "\t" + billingAddress["city"]
            + "\t" + billingAddress["state"]
            + "\t" + billingAddress["postalCode"]
        const shippingAddressString
            = shippingAddress["name"]
            + "\t" + shippingAddress["line1"]
            + "\t" + shippingAddress["line2"]
            + "\t" + shippingAddress["city"]
            + "\t" + shippingAddress["state"]
            + "\t" + shippingAddress["postalCode"]

        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/invoice/checkout?creditCard=${encodeURIComponent(creditCardString)}&billingAddress=${encodeURIComponent(billingAddressString)}&shippingAddress=${encodeURIComponent(shippingAddressString)}`, { headers })
            if (response.ok) {
                const data = await response.json()
                const destination = data.InvoiceId
                navigate(`/invoice/${destination}`)
                window.location.reload()
            } else if (response.status === 400) {
                setStatus(400)
            } else if (response.status === 401) {
                setStatus(401)
                navigate('/unauthorized')
                window.location.reload()
            } else if (response.status === 404) {
                setStatus(404)
                navigate('/not-found')
                window.location.reload()
            } else {
                setStatus(500)
            }
        }
        catch (Exception) {
            setStatus(500)
        }
    }

    let validForm = true;

    if (creditCard["cardNumber"] != null) {
        if (CreditCardRegex.test(creditCard["cardNumber"])) {
            validation["creditCardNumber"] = 1;
        } else {
            validation["creditCardNumber"] = 0;
            validForm = false;
        }
    }

    if (creditCard["verificationCode"] != null) {
        if (CreditCardVCRegex.test(creditCard["verificationCode"])) {
            validation["creditCardVC"] = 1;
        } else {
            validation["creditCardVC"] = 0;
            validForm = false;
        }
    }

    if (creditCard["expirationMonth"] != null) {
        if (CreditCardExpMonthRegex.test(creditCard["expirationMonth"])) {
            validation["creditCardExpirationMonth"] = 1;
        } else {
            validation["creditCardExpirationMonth"] = 0;
            validForm = false;
        }
    }

    if (creditCard["expirationYear"] != null) {
        if (CreditCardExpYearRegex.test(creditCard["expirationYear"])) {
            validation["creditCardExpirationYear"] = 1;
        } else {
            validation["creditCardExpirationYear"] = 0;
            validForm = false;
        }
    }

    if (billingAddress["name"] != null) {
        if (NameRegex.test(billingAddress["name"])) {
            validation["billingName"] = 1;
        } else {
            validation["billingName"] = 0;
            validForm = false;
        }
    }

    if (billingAddress["line1"] != null) {
        if (Line1Regex.test(billingAddress["line1"])) {
            validation["billingLine1"] = 1;
        } else {
            validation["billingLine1"] = 0;
            validForm = false;
        }
    }

    if (billingAddress["line2"] != null) {
        if (Line2Regex.test(billingAddress["line2"])) {
            validation["billingLine2"] = 1;
        } else {
            validation["billingLine2"] = 0;
            validForm = false;
        }
    }

    if (billingAddress["line2"] == null) {
        validation["billingLine2"] = 1;
    }

    if (billingAddress["city"] != null) {
        if (CityRegex.test(billingAddress["city"])) {
            validation["billingCity"] = 1;
        } else {
            validation["billingCity"] = 0;
            validForm = false;
        }
    }

    if (billingAddress["state"] != null) {
        if (StateRegex.test(billingAddress["state"])) {
            validation["billingState"] = 1;
        } else {
            validation["billingState"] = 0;
            validForm = false;
        }
    }

    if (billingAddress["postalCode"] != null) {
        if (PostalRegex.test(billingAddress["postalCode"])) {
            validation["billingPostal"] = 1;
        } else {
            validation["billingPostal"] = 0;
            validForm = false;
        }
    }

    if (shippingAddress["name"] != null) {
        if (NameRegex.test(shippingAddress["name"])) {
            validation["shippingName"] = 1;
        } else {
            validation["shippingName"] = 0;
            validForm = false;
        }
    }

    if (shippingAddress["line1"] != null) {
        if (Line1Regex.test(shippingAddress["line1"])) {
            validation["shippingLine1"] = 1;
        } else {
            validation["shippingLine1"] = 0;
            validForm = false;
        }
    }

    if (shippingAddress["line2"] != null) {
        if (Line2Regex.test(shippingAddress["line2"])) {
            validation["shippingLine2"] = 1;
        } else {
            validation["shippingLine2"] = 0;
            validForm = false;
        }
    }

    if (shippingAddress["line2"] == null) {
        validation["shippingLine2"] = 1;
    }

    if (shippingAddress["city"] != null) {
        if (CityRegex.test(shippingAddress["city"])) {
            validation["shippingCity"] = 1;
        } else {
            validation["shippingCity"] = 0;
            validForm = false;
        }
    }

    if (shippingAddress["state"] != null) {
        if (StateRegex.test(shippingAddress["state"])) {
            validation["shippingState"] = 1;
        } else {
            validation["shippingState"] = 0;
            validForm = false;
        }
    }

    if (shippingAddress["postalCode"] != null) {
        if (PostalRegex.test(shippingAddress["postalCode"])) {
            validation["shippingPostal"] = 1;
        } else {
            validation["shippingPostal"] = 0;
            validForm = false;
        }
    }

    if (validation["creditCardExpirationYear"] === 1 && validation["creditCardExpirationMonth"] === 1) {
        let expDate = new Date(Number.parseInt(creditCard["expirationYear"]), Number.parseInt(creditCard["expirationMonth"]) - 1)
        let currDate = new Date()
        if (expDate.getFullYear() > currDate.getFullYear() || (expDate.getFullYear() === currDate.getFullYear() && expDate.getMonth() >= currDate.getMonth())) {
            validation["creditCardExpirationMonth"] = 1;
            validation["creditCardExpirationYear"] = 1;
        } else {
            validation["creditCardExpirationMonth"] = 0;
            validation["creditCardExpirationYear"] = 0;
            validForm = false;
        }
    }

    return (
        <div className="d-flex flex-wrap justify-content-center" style={{ gridColumnGap: "100%" }}>
            <Card style={{ margin: "10px", maxWidth: "40rem", width: "40rem" }}>
                <CardBody>
                    <Row>
                        <CardSubtitle style={{ textAlign: "center" }} tag="h4">Checkout</CardSubtitle>
                    </Row>
                    <br />
                    <Row>
                        <CardSubtitle tag="h4">Total: ${total}</CardSubtitle>
                    </Row>
                    <br />
                    <Form onSubmit={HandleSubmit}>
                        <Row>
                            <CardSubtitle tag="h5">Enter Payment Information</CardSubtitle>
                        </Row>
                        <br />
                        <Row>
                            <FormGroup>
                                <Label for="creditCardNumber">Credit Card Number</Label>
                                <Input id="creditCardNumber" required invalid={validation["creditCardNumber"] === 0} value={creditCard["cardNumber"]} onChange={e => handleCreditCard("cardNumber", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="creditCardVC">CVC</Label>
                                    <Input id="creditCardVC" required invalid={validation["creditCardVC"] === 0} value={creditCard["verificationCode"]} onChange={e => handleCreditCard("verificationCode", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="creditCardExpMM">Exp MM</Label>
                                    <Input id="creditCardExpMM" required invalid={validation["creditCardExpirationMonth"] === 0} value={creditCard["expirationMonth"]} onChange={e => handleCreditCard("expirationMonth", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="creditCardExpYYYY">Exp YYYY</Label>
                                    <Input id="creditCardExpYYYY" required invalid={validation["creditCardExpirationYear"] === 0} value={creditCard["expirationYear"]} onChange={e => handleCreditCard("expirationYear", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <CardSubtitle tag="h5">Enter Billing Address</CardSubtitle>
                        </Row>
                        <br />
                        <Row>
                            <FormGroup>
                                <Label for="billingName">Name</Label>
                                <Input id="billingName" required invalid={validation["billingName"] === 0} value={billingAddress["name"]} onChange={e => handleBillingAddress("name", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <FormGroup>
                                <Label for="billingLine1">Line 1</Label>
                                <Input id="billingLine1" required invalid={validation["billingLine1"] === 0} value={billingAddress["line1"]} onChange={e => handleBillingAddress("line1", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <FormGroup>
                                <Label for="billingLine2">Line 2</Label>
                                <Input id="billingLine2" invalid={validation["billingLine2"] === 0} value={billingAddress["line2"]} onChange={e => handleBillingAddress("line2", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <Col xs="6">
                                <FormGroup>
                                    <Label for="billingCity">City</Label>
                                    <Input id="billingCity" required invalid={validation["billingCity"] === 0} value={billingAddress["city"]} onChange={e => handleBillingAddress("city", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="billingState">State</Label>
                                    <Input id="billingState" required invalid={validation["billingState"] === 0} value={billingAddress["state"]} onChange={e => handleBillingAddress("state", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="billingPostal">ZIP</Label>
                                    <Input id="billingPostal" required invalid={validation["billingPostal"] === 0} value={billingAddress["postalCode"]} onChange={e => handleBillingAddress("postalCode", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <CardSubtitle tag="h5">Enter Shipping Address</CardSubtitle>
                        </Row>
                        <br />
                        <Row>
                            <FormGroup>
                                <Label for="shippingName">Name</Label>
                                <Input id="shippingName" required invalid={validation["shippingName"] === 0} value={shippingAddress["name"]} onChange={e => handleShippingAddress("name", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <FormGroup>
                                <Label for="shippingLine1">Line 1</Label>
                                <Input id="shippingLine1" required invalid={validation["shippingLine1"] === 0} value={shippingAddress["line1"]} onChange={e => handleShippingAddress("line1", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <FormGroup>
                                <Label for="shippingLine2">Line 2</Label>
                                <Input id="shippingLine2" invalid={validation["shippingLine2"] === 0} value={shippingAddress["line2"]} onChange={e => handleShippingAddress("line2", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <Col xs="6">
                                <FormGroup>
                                    <Label for="shippingCity">City</Label>
                                    <Input id="shippingCity" required invalid={validation["shippingCity"] === 0} value={shippingAddress["city"]} onChange={e => handleShippingAddress("city", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="shippingState">State</Label>
                                    <Input id="shippingState" required invalid={validation["shippingState"] === 0} value={shippingAddress["state"]} onChange={e => handleShippingAddress("state", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="shippingPostal">ZIP</Label>
                                    <Input id="shippingPostal" required invalid={validation["shippingPostal"] === 0} value={shippingAddress["postalCode"]} onChange={e => handleShippingAddress("postalCode", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Alert color="danger" isOpen={status === 400}> Form input is invalid. </Alert>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Alert color="warning" isOpen={status === 500}> There was an error while processing your request. </Alert>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Button block color="login" type="submit" disabled={!validForm}>
                                    Checkout
                                </Button>
                            </Col>
                        </Row>
                    </Form>
                </CardBody>
            </Card>
        </div>
    )
}