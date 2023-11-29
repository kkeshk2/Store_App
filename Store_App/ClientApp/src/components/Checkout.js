import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import {
    Alert,
    Input,
    Label,
    Form,
    FormGroup,
    FormText,
    FormFeedback,
    Row,
    Col,
    Button,
    Nav,
    NavItem,
    NavLink,
    Card,
    CardBody,
    CardTitle,
    CardSubtitle
} from 'reactstrap'

export default function Checkout() {
    const [fields, setFields] = useState({})
    const [validation, setValidation] = useState({})
    const [status, setStatus] = useState(0)
    const [cart, setCart] = useState([])
    const [total, setTotal] = useState(0)
    const navigate = useNavigate()

    const CreditCardRegex = /^[0-9]{16,16}$/
    const CreditCardVCRegex = /^[0-9]{3,4}$/
    const CreditCardExpMonthRegex = /^[0-9]{2,2}$/
    const CreditCardExpYearRegex = /^[0-9]{4,4}$/
    const NameRegex = /^[A-Za-z][A-Za-z\.\-\x20]{0,127}$/
    const Line1Regex = /^[A-Za-z0-9][A-Za-z0-9\.\-\&\x20]{0,127}$/
    const Line2Regex = /^[A-Za-z0-9]?[A-Za-z0-9\.\-\&\x20]{0,127}$/
    const CityRegex = /^[A-Za-z0-9][A-Za-z0-9\.\-\&\x20]{0,127}$/
    const StateRegex = /^[A-Z]{2,2}$/
    const PostalRegex = /^[0-9]{5,5}$/

    useEffect(() => {
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
        getTotal();
    }, []);

    const handleChange = (field, value) => {
        setFields({
            ...fields,
            [field]: value
        })
    }

    const HandleSubmit = async event => {
        event.preventDefault();
        const URICreditCard = encodeURIComponent(fields["creditCardNumber"])
        const URICreditCardVC = encodeURIComponent(fields["creditCardVC"])
        const URICreditCardExpMM = encodeURIComponent(fields["creditCardExpMM"])
        const URICreditCardExpYYYY = encodeURIComponent(fields["creditCardExpYYYY"])
        const URIBillingName = encodeURIComponent(fields["billingName"])
        const URIBillingLine1 = encodeURIComponent(fields["billingLine1"])
        const URIBillingLine2 = encodeURIComponent(fields["billingLine2"])
        const URIBillingCity = encodeURIComponent(fields["billingCity"])
        const URIBillingState = encodeURIComponent(fields["billingState"])
        const URIBillingPostal = encodeURIComponent(fields["billingPostal"])
        const URIShippingName = encodeURIComponent(fields["shippingName"])
        const URIShippingLine1 = encodeURIComponent(fields["shippingLine1"])
        const URIShippingLine2 = encodeURIComponent(fields["shippingLine2"])
        const URIShippingCity = encodeURIComponent(fields["shippingCity"])
        const URIShippingState = encodeURIComponent(fields["shippingState"])
        const URIShippingPostal = encodeURIComponent(fields["shippingPostal"])
        const URICreditCardFull = URICreditCard + "%1F" + URICreditCardVC + "%1F" + URICreditCardExpMM + "%1F" + URICreditCardExpYYYY
        const URIBillingAddress = URIBillingName + "%1F" + URIBillingLine1 + "%1F" + URIBillingLine2 + "%1F" + URIBillingCity + "%1F" + URIBillingState + "%1F" + URIBillingPostal
        const URIShippingAddress = URIShippingName + "%1F" + URIShippingLine1 + "%1F" + URIShippingLine2 + "%1F" + URIShippingCity + "%1F" + URIShippingState + "%1F" + URIShippingPostal

        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/invoice/checkout?creditCard=${URICreditCardFull}&billingAddress=${URIBillingAddress}&shippingAddress=${URIShippingAddress}`, { headers })
            if (response.ok) {
                const data = await response.json()
                const destination = data.InvoiceId
                navigate(`/invoice/${destination}`)
                window.location.reload()
            } else if (response.status === 400) {
                setStatus(400)
            } else if (response.status === 401) {
                setStatus(401)
            } else {
                setStatus(502)
            }
        }
        catch (Exception) {
            setStatus(502)
        }
    }

    let validForm = true;

    if (fields["creditCardNumber"] != null) {
        if (CreditCardRegex.test(fields["creditCardNumber"])) {
            validation["creditCardNumber"] = 1;
        } else {
            validation["creditCardNumber"] = 0;
            validForm = false;
        }
    }

    if (fields["creditCardVC"] != null) {
        if (CreditCardVCRegex.test(fields["creditCardVC"])) {
            validation["creditCardVC"] = 1;
        } else {
            validation["creditCardVC"] = 0;
            validForm = false;
        }
    }

    if (fields["creditCardExpMM"] != null) {
        if (CreditCardExpMonthRegex.test(fields["creditCardExpMM"])) {
            validation["creditCardExpMM"] = 1;
        } else {
            validation["creditCardExpMM"] = 0;
            validForm = false;
        }
    }

    if (fields["creditCardExpYYYY"] != null) {
        if (CreditCardExpYearRegex.test(fields["creditCardExpYYYY"])) {
            validation["creditCardExpYYYY"] = 1;
        } else {
            validation["creditCardExpYYYY"] = 0;
            validForm = false;
        }
    }

    if (fields["billingName"] != null) {
        if (NameRegex.test(fields["billingName"])) {
            validation["billingName"] = 1;
        } else {
            validation["billingName"] = 0;
            validForm = false;
        }
    }

    if (fields["billingLine1"] != null) {
        if (Line1Regex.test(fields["billingLine1"])) {
            validation["billingLine1"] = 1;
        } else {
            validation["billingLine1"] = 0;
            validForm = false;
        }
    }

    if (fields["billingLine2"] != null) {
        if (Line2Regex.test(fields["billingLine2"])) {
            validation["billingLine2"] = 1;
        } else {
            validation["billingLine2"] = 0;
            validForm = false;
        }
    }

    if (fields["billingLine2"] == null) {
        validation["billingLine2"] = 1;
    }

    if (fields["billingCity"] != null) {
        if (CityRegex.test(fields["billingCity"])) {
            validation["billingCity"] = 1;
        } else {
            validation["billingCity"] = 0;
            validForm = false;
        }
    }

    if (fields["billingState"] != null) {
        if (StateRegex.test(fields["billingState"])) {
            validation["billingState"] = 1;
        } else {
            validation["billingState"] = 0;
            validForm = false;
        }
    }

    if (fields["billingState"] != null) {
        if (StateRegex.test(fields["billingState"])) {
            validation["billingState"] = 1;
        } else {
            validation["billingState"] = 0;
            validForm = false;
        }
    }

    if (fields["billingPostal"] != null) {
        if (PostalRegex.test(fields["billingPostal"])) {
            validation["billingPostal"] = 1;
        } else {
            validation["billingPostal"] = 0;
            validForm = false;
        }
    }

    if (fields["shippingName"] != null) {
        if (NameRegex.test(fields["shippingName"])) {
            validation["shippingName"] = 1;
        } else {
            validation["shippingName"] = 0;
            validForm = false;
        }
    }

    if (fields["shippingLine1"] != null) {
        if (Line1Regex.test(fields["shippingLine1"])) {
            validation["shippingLine1"] = 1;
        } else {
            validation["shippingLine1"] = 0;
            validForm = false;
        }
    }

    if (fields["shippingLine2"] != null) {
        if (Line2Regex.test(fields["shippingLine2"])) {
            validation["shippingLine2"] = 1;
        } else {
            validation["shippingLine2"] = 0;
            validForm = false;
        }
    }

    if (fields["shippingLine2"] == null) {
        validation["shippingLine2"] = 1;
    }

    if (fields["shippingCity"] != null) {
        if (CityRegex.test(fields["shippingCity"])) {
            validation["shippingCity"] = 1;
        } else {
            validation["shippingCity"] = 0;
            validForm = false;
        }
    }

    if (fields["shippingState"] != null) {
        if (StateRegex.test(fields["shippingState"])) {
            validation["shippingState"] = 1;
        } else {
            validation["shippingState"] = 0;
            validForm = false;
        }
    }

    if (fields["shippingState"] != null) {
        if (StateRegex.test(fields["shippingState"])) {
            validation["shippingState"] = 1;
        } else {
            validation["shippingState"] = 0;
            validForm = false;
        }
    }

    if (fields["shippingPostal"] != null) {
        if (PostalRegex.test(fields["shippingPostal"])) {
            validation["shippingPostal"] = 1;
        } else {
            validation["shippingPostal"] = 0;
            validForm = false;
        }
    }

    if (validation["creditCardExpYYYY"] === 1 && validation["creditCardExpMM"] === 1) {
        let expDate = new Date(Number.parseInt(fields["creditCardExpYYYY"]), Number.parseInt(fields["creditCardExpMM"]) - 1)
        let currDate = new Date()
        if (expDate.getFullYear() > currDate.getFullYear() || (expDate.getFullYear() === currDate.getFullYear() && expDate.getMonth() >= currDate.getMonth())) {
            validation["creditCardExpMM"] = 1;
            validation["creditCardExpYYYY"] = 1;
        } else {
            validation["creditCardExpMM"] = 0;
            validation["creditCardExpYYYY"] = 0;
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
                                <Input id="creditCardNumber" required invalid={validation["creditCardNumber"] === 0} value={fields["creditCardNumber"]} onChange={e => handleChange("creditCardNumber", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <Col xs="3">
                                <FormGroup>
                                <Label for="creditCardVC">CVC</Label>
                                    <Input id="creditCardVC" required invalid={validation["creditCardVC"] === 0} value={fields["creditCardVC"]} onChange={e => handleChange("creditCardVC", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="creditCardExpMM">Exp MM</Label>
                                    <Input id="creditCardExpMM" required invalid={validation["creditCardExpMM"] === 0} value={fields["creditCardExpMM"]} onChange={e => handleChange("creditCardExpMM", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="creditCardExpYYYY">Exp YYYY</Label>
                                    <Input id="creditCardExpYYYY" required invalid={validation["creditCardExpYYYY"] === 0} value={fields["creditCardExpYYYY"]} onChange={e => handleChange("creditCardExpYYYY", e.target.value)}></Input>
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
                                <Input id="billingName" required invalid={validation["billingName"] === 0} value={fields["billingName"]} onChange={e => handleChange("billingName", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <FormGroup>
                                <Label for="billingLine1">Line 1</Label>
                                <Input id="billingLine1" required invalid={validation["billingLine1"] === 0} value={fields["billingLine1"]} onChange={e => handleChange("billingLine1", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <FormGroup>
                                <Label for="billingLine2">Line 2</Label>
                                <Input id="billingLine2" invalid={validation["billingLine2"] === 0} value={fields["billingLine2"]} onChange={e => handleChange("billingLine2", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <Col xs="6">
                                <FormGroup>
                                    <Label for="billingCity">City</Label>
                                    <Input id="billingCity" required invalid={validation["billingCity"] === 0} value={fields["billingCity"]} onChange={e => handleChange("billingCity", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="billingState">State</Label>
                                    <Input id="billingState" required invalid={validation["billingState"] === 0} value={fields["billingState"]} onChange={e => handleChange("billingState", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="billingPostal">ZIP</Label>
                                    <Input id="billingPostal" required invalid={validation["billingPostal"] === 0} value={fields["billingPostal"]} onChange={e => handleChange("billingPostal", e.target.value)}></Input>
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
                                <Input id="shippingName" required invalid={validation["shippingName"] === 0} value={fields["shippingName"]} onChange={e => handleChange("shippingName", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <FormGroup>
                                <Label for="shippingLine1">Line 1</Label>
                                <Input id="shippingLine1" required invalid={validation["shippingLine1"] === 0} value={fields["shippingLine1"]} onChange={e => handleChange("shippingLine1", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <FormGroup>
                                <Label for="shippingLine2">Line 2</Label>
                                <Input id="shippingLine2" invalid={validation["shippingLine2"] === 0} value={fields["shippingLine2"]} onChange={e => handleChange("shippingLine2", e.target.value)}></Input>
                            </FormGroup>
                        </Row>
                        <Row>
                            <Col xs="6">
                                <FormGroup>
                                    <Label for="shippingCity">City</Label>
                                    <Input id="shippingCity" required invalid={validation["shippingCity"] === 0} value={fields["shippingCity"]} onChange={e => handleChange("shippingCity", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="shippingState">State</Label>
                                    <Input id="shippingState" required invalid={validation["shippingState"] === 0} value={fields["shippingState"]} onChange={e => handleChange("shippingState", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                            <Col xs="3">
                                <FormGroup>
                                    <Label for="shippingPostal">ZIP</Label>
                                    <Input id="shippingPostal" required invalid={validation["shippingPostal"] === 0} value={fields["shippingPostal"]} onChange={e => handleChange("shippingPostal", e.target.value)}></Input>
                                </FormGroup>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Button block color="login" type="submit">
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