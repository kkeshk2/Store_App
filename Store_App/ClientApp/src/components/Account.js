import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import {
    Alert,
    Input,
    Form,
    FormGroup,
    FormFeedback,
    Row,
    Col,
    Button,
    Card,
    CardBody,
    CardTitle,
    Modal,
    ModalHeader,
    ModalBody,
} from 'reactstrap'

export default function Account() {
    const emailRegex = /^[^@\s@]+@[^@\s]+\.[^@\s]+$/
    const passRegex = /^[^\s]{8,128}$/

    const [fields, setFields] = useState({})
    const [validation, setValidation] = useState({})
    const [status, setStatus] = useState({})
    const navigate = useNavigate()
    const [account, setAccount] = useState([])

    const [emailModal, setEmailModal] = useState(false)
    const [passwordModal, setPasswordModal] = useState(false)

    const toggleEmailModal = () => {
        setEmailModal(!emailModal)
        setStatus(0)
        setFields({
            ...fields,
            [`email`]: null
        })
        validation["email"] = null
    }

    const togglePasswordModal = () => {
        setPasswordModal(!passwordModal)
        setStatus(0)
        setFields({
            ...fields,
            [`password`]: null,
            [`password2`]: null
        })
        validation["password"] = null
        validation["password2"] = null
    }

    useEffect(() => {
        const fetchAccount = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/account/accessaccount`, { headers });
                const data = await response.json();
                setAccount(data);
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
        fetchAccount();
    }, [navigate]);

    const handleChange = (field, value) => {
        setFields({
            ...fields,
            [field]: value
        })
    }

    const HandleSubmitEmail = async event => {
        event.preventDefault();
        const URIEmail = encodeURIComponent(fields["email"])

        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/account/updateaccountemail?email=${URIEmail}`, { headers });
            if (response.ok) {
                navigate("/account")
                window.location.reload()
            } else if (response.status === 400) {
                setStatus({
                    ...status,
                    [`email`]: 400
                })
            } else if (response.status === 403) {
                setStatus({
                    ...status,
                    [`email`]: 403
                })
            } else if (response.status === 401) {
                navigate("/unauthorized")
                window.location.reload()
            } else {
                setStatus({
                    ...status,
                    [`email`]: 500
                })
            }
        }
        catch (Exception) {
            setStatus({
                ...status,
                [`email`]: 500
            })
        }
    }

    const HandleSubmitPassword = async event => {
        event.preventDefault();
        const URIPassword = encodeURIComponent(fields["password"])
        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/account/updateaccountpassword?password=${URIPassword}`, { headers });
            if (response.ok) {
                navigate("/account")
                window.location.reload()
            } else if (response.status === 400) {
                setStatus({
                    ...status,
                    [`password`]: 400
                })
            } else if (response.status === 401) {
                navigate("/unauthorized")
                window.location.reload()
            } else {
                setStatus({
                    ...status,
                    [`password`]: 500
                })
            }
        }
        catch (Exception) {
            setStatus({
                ...status,
                [`password`]: 500
            })
        }
    }

    let validEmail = true;
    let validPassword = true;

    if (fields["email"] != null) {
        if (emailRegex.test(fields["email"])) {
            validation["email"] = 1
        } else {
            validation["email"] = 0
            validEmail = false;
        }
    }

    if (fields["password"] != null) {
        if (passRegex.test(fields["password"])) {
            validation["password"] = 1
        } else {
            validation["password"] = 0
            validPassword = false;
        }
    }

    if (fields["password2"] != null) {
        if (fields["password"] === fields["password2"]) {
            validation["password2"] = 1
        } else {
            validation["password2"] = 0
            validPassword = false;
        }
    }


    return (
        <div className="d-flex flex-wrap justify-content-center" style={{ gridColumnGap: "100%" }} >
            <Modal isOpen={emailModal} toggle={toggleEmailModal} style={{maxWidth: "20rem"} }>
                <ModalHeader toggle={toggleEmailModal}>
                    Update Email
                </ModalHeader>
                <ModalBody>
                    <Form onSubmit={HandleSubmitEmail}>
                        <Alert color="danger" isOpen={status["email"] === 400}> Invalid Input. </Alert>
                        <Alert color="danger" isOpen={status["email"] === 403}> That email is already taken. </Alert>
                        <Alert color="warning" isOpen={status["email"] === 500}> There was an error while updating your email. </Alert>
                        <FormGroup>
                            <Input id="email" type="email" placeholder="Email" required value={fields["email"]} onChange={e => handleChange("email", e.target.value)} invalid={validation["email"] === 0} />
                            <FormFeedback invalid>A valid email address is required.</FormFeedback>
                        </FormGroup>
                        <FormGroup>
                            <Button block type="submit" color="login" disabled={validEmail === false}>Update Email</Button>
                        </FormGroup>
                    </Form>
                </ModalBody>
            </Modal>
            <Modal isOpen={passwordModal} toggle={togglePasswordModal} style={{maxWidth: "20rem"} }>
                <ModalHeader toggle={togglePasswordModal}>
                    Update Password
                </ModalHeader>
                <ModalBody>
                    <Form onSubmit={HandleSubmitPassword}>
                        <Alert color="danger" isOpen={status["password"] === 400}> Invalid Input. </Alert>
                        <Alert color="warning" isOpen={status["password"] === 500}> There was an error while updating your password. </Alert>
                        <FormGroup>
                            <Input id="password" type="password" placeholder="Password" required value={fields["password"]} onChange={e => handleChange("password", e.target.value)} invalid={validation["password"] === 0} />
                            <FormFeedback invalid>Passwords must contain at least 8 characters and no whitespace.</FormFeedback>
                        </FormGroup>
                        <FormGroup>
                            <Input id="password2" type="password" placeholder="Confirm Password" required value={fields["password2"]} onChange={e => handleChange("password2", e.target.value)} invalid={validation["password2"] === 0} />
                            <FormFeedback invalid>Passwords do not match.</FormFeedback>
                        </FormGroup>
                        <FormGroup>
                            <Button block type="submit" color="login" disabled={validPassword === false}>Update Password</Button>
                        </FormGroup>
                    </Form>
                </ModalBody>
            </Modal>
            <Card style={{ maxWidth: '40rem', width: '40rem'}}>
                <CardBody>
                    <CardTitle style={{ padding: '10px' }} tag="h4">
                        Manage Account
                    </CardTitle>
                    <Row style={{ padding: '10px' }}>
                        <Col xs="3">
                            Email:
                        </Col>
                        <Col xs="6">
                            {account.Email}
                        </Col>
                        <Col xs="3" style={{ textAlign: "right" }}>
                            <Button color="login" size="sm" type="auto" onClick={toggleEmailModal}>
                                Update
                            </Button>
                        </Col>
                    </Row>
                    <Row style={{ padding: '10px' }}>
                        <Col xs="3">
                            Password:
                        </Col>
                        <Col xs="6">
                            **********
                        </Col>
                        <Col xs="3" style={{textAlign: "right"}}>
                            <Button color="login" size="sm" type="auto" onClick={togglePasswordModal}>
                                Update
                            </Button>
                        </Col>
                    </Row>
                </CardBody>
            </Card>

        </div>
    );
}