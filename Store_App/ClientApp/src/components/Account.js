import React, { Component, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import {
    Alert,
    ButtonGroup,
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
    Modal,
    ModalHeader,
    ModalBody,
    ModalFooter
} from 'reactstrap'

export default function Account() {
    const emailRegex = /^[^@\s@]+@[^@\s]+\.[^@\s]+$/
    const passRegex = /^[^\s]{8,128}$/
    const nameRegex = /^[A-Za-z][A-Za-z\.\-\x20]{0,127}$/

    const [fields, setFields] = useState({})
    const [validation, setValidation] = useState({})
    const [status, setStatus] = useState({})
    const navigate = useNavigate()
    const [account, setAccount] = useState([])

    const [emailModal, setEmailModal] = useState(false)
    const [nameModal, setNameModal] = useState(false)
    const [passwordModal, setPasswordModal] = useState(false)

    const toggleEmailModal = () => {
        setEmailModal(!emailModal)
        setFields({
            ...fields,
            [`email`]: null
        })
        validation["email"] = null
    }

    const toggleNameModal = () => {
        setNameModal(!nameModal)
        setFields({
            ...fields,
            [`name`]: null
        })
        validation["name"] = null
    }

    const togglePasswordModal = () => {
        setPasswordModal(!passwordModal)
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
                const response = await fetch(`api/account/getaccount`, { headers });
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
    }, []);

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
            } else if (response.status === 401) {
                setStatus({
                    ...status,
                    [`email`]: 401
                })
            } else {
                setStatus({
                    ...status,
                    [`email`]: 502
                })
            }
        }
        catch (Exception) {
            setStatus({
                ...status,
                [`email`]: 502
            })
        }
    }

    const HandleSubmitName = async event => {
        event.preventDefault();
        const URIName = encodeURIComponent(fields["name"])
        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/account/updateaccountname?name=${URIName}`, { headers });
            if (response.ok) {
                navigate("/account")
                window.location.reload()
            } else if (response.status === 400) {
                setStatus({
                    ...status,
                    [`name`]: 400
                })
            } else if (response.status === 401) {
                setStatus({
                    ...status,
                    [`name`]: 401
                })
            } else {
                setStatus({
                    ...status,
                    [`name`]: 502
                })
            }
        }
        catch (Exception) {
            setStatus({
                ...status,
                [`name`]: 502
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
                setStatus({
                    ...status,
                    [`password`]: 401
                })
            } else {
                setStatus({
                    ...status,
                    [`password`]: 502
                })
            }
        }
        catch (Exception) {
            setStatus({
                ...status,
                [`password`]: 502
            })
        }
    }

    let validEmail = true;
    let validPassword = true;
    let validName = true;

    if (fields["email"] != null) {
        if (emailRegex.test(fields["email"])) {
            validation["email"] = 1
        } else {
            validation["email"] = 0
            validEmail = false;
        }
    }

    if (fields["name"] != null) {
        if (nameRegex.test(fields["name"])) {
            validation["name"] = 1
        } else {
            validation["name"] = 0
            validName = false;
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
        <div>
            <Modal isOpen={emailModal} toggle={toggleEmailModal} style={{maxWidth: "20rem"} }>
                <ModalHeader toggle={toggleEmailModal}>
                    Update Email
                </ModalHeader>
                <ModalBody>
                    <Form onSubmit={HandleSubmitEmail}>
                        <Alert color="danger" isOpen={status["email"] === 400}> Invalid Input. </Alert>
                        <Alert color="danger" isOpen={status["email"] === 401}> That email is already taken. </Alert>
                        <Alert color="warning" isOpen={status["email"] === 502}> There was an error while updating your email. </Alert>
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
            <Modal isOpen={nameModal} toggle={toggleNameModal} style={{ maxWidth: "20rem" }}>
                <ModalHeader toggle={toggleNameModal}>
                    Update Name
                </ModalHeader>
                <ModalBody>
                    <Form onSubmit={HandleSubmitName}>
                        <Alert color="danger" isOpen={status["name"] === 400}> Invalid Input. </Alert>
                        <Alert color="danger" isOpen={status["name"] === 401}> Invalid Credentials. </Alert>
                        <Alert color="warning" isOpen={status["name"] === 502}> There was an error while updating your name. </Alert>
                        <FormGroup>
                            <Input id="name" placeholder="Name" required value={fields["name"]} onChange={e => handleChange("name", e.target.value)} invalid={validation["name"] === 0} />
                            <FormFeedback invalid>Names must start with a letter.<br></br><br></br> Names may not contain numbers or special characters except "." and "-".</FormFeedback>
                        </FormGroup>
                        <FormGroup>
                            <Button block type="submit" color="login" disabled={validName === false}>Update Name</Button>
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
                        <Alert color="danger" isOpen={status["password"] === 401}> Invalid Credentials. </Alert>
                        <Alert color="warning" isOpen={status["password"] === 502}> There was an error while updating your password. </Alert>
                        <FormGroup>
                            <Input id="password" type="password" placeholder="Password" required value={fields["password"]} onChange={e => handleChange("password", e.target.value)} invalid={validation["password"] === 0} />
                            <FormFeedback invalid>Passwords must contain at least 8 characters and no whitespace.</FormFeedback>
                        </FormGroup>
                        <FormGroup>
                            <Input id="password2" type="password" placeholder="Confirm Password" required value={fields["password2"]} onChange={e => handleChange("password2", e.target.value)} invalid={validation["password2"] === 0} />
                            <FormFeedback invalid>Passwords do not match.</FormFeedback>
                        </FormGroup>
                        <FormGroup>
                            <Button block type="submit" color="login" disabled={validPassword === false}>Password</Button>
                        </FormGroup>
                    </Form>
                </ModalBody>
            </Modal>
            <Card style={{ maxWidth: '40rem'}}>
                <CardBody>
                    <CardTitle style={{ padding: '10px' }} tag="h4">
                        Manage Account
                    </CardTitle>
                    <Row style={{ padding: '10px' }}>
                        <Col xs="3">
                            Email:
                        </Col>
                        <Col xs="6">
                            {account.AccountEmail}
                        </Col>
                        <Col xs="3" style={{ textAlign: "right" }}>
                            <Button color="login" size="sm" type="auto" onClick={toggleEmailModal}>
                                Update
                            </Button>
                        </Col>
                    </Row>
                    <Row style={{ padding: '10px' }}>
                        <Col xs="3">
                            Name:
                        </Col>
                        <Col xs="6">
                            {account.AccountName}
                        </Col>
                        <Col xs="3" style={{ textAlign: "right" }}>
                            <Button color="login" size="sm" type="auto" onClick={toggleNameModal}>
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