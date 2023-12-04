import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {
    Input,
    Row,
    Col,
    Button,
    ButtonGroup,
    Card,
    CardBody,
    CardTitle,
    CardSubtitle,
    AccordionBody,
    AccordionHeader,
    AccordionItem,
    UncontrolledAccordion,
    Table,
    Modal,
    ModalFooter,
    ModalHeader,
    ModalBody
} from 'reactstrap'
import '../custom.css';

function Product() {
    const [product, setProduct] = useState(null);
    const [loading, setLoading] = useState(true);
    const [cartContains, setCartContains] = useState(0);
    const { id: productId } = useParams();
    const [selectedQuantity, setSelectedQuantity] = useState(1);
    const navigate = useNavigate();
    const [modal, setModal] = useState(false);

    let verified = true

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(`api/product/accessproduct?productId=${productId}`);
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
                console.log('NETWORK GOOD');
                console.log(data);
                setProduct(data);
            } catch (error) {
                console.error('Error fetching product:', error);
            } finally {
                setLoading(false);
            }
        };

        const fetchCart = async () => {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/cart/accesscart`, { headers });
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
                console.log('NETWORK GOOD');
                console.log(data);
                const products = data.Products
                for (let x in products) {
                    if (parseInt(products[x].Product.ProductId) === parseInt(productId)) {
                        setCartContains(products[x].Quantity)
                        setSelectedQuantity(products[x].Quantity)
                    }
                }
            } catch (error) {
                console.error('Error fetching cart:', error);
            }
        };

        const verifyUser = async () => {
            if (localStorage.getItem("authtoken")) {
                try {
                    const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                    const response = await fetch(`api/account/verifyaccount`, { headers });
                    if (!response.ok) {
                        localStorage.removeItem("authtoken")
                        window.location.reload()
                    }
                } catch (Exception) {
                    localStorage.removeItem("authtoken")
                    window.location.reload()
                }
            }
        }

        fetchData()
        verifyUser()
        if (localStorage.getItem("authtoken")) {
            fetchCart()
        } else {
            setCartContains(0)
        }
    }, [productId]);

    if (!localStorage.getItem("authtoken")) {
        verified = false
    }

    
    const handleQuantity = (event) => {
        setSelectedQuantity(event.target.value)
    }

    const addToCart = async () => {
        if (verified === false) {
            setModal(true)
        } else {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/cart/addtocart?productId=${productId}&quantity=${selectedQuantity}`, { headers });
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
                console.log('NETWORK GOOD');
                console.log(data);
            } catch (error) {
                console.error('Error adding to cart:', error);
            }
            console.log(`Product ${product.Name} added to the cart`);
            window.location.reload()
        }
    };

    const deleteFromCart = async () => {
        try {
            const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
            const response = await fetch(`api/cart/deletefromcart?productId=${productId}`, { headers });
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
            console.log('NETWORK GOOD');
            console.log(data);
        } catch (error) {
            console.error('Error adding to cart:', error);
        }
        console.log(`Item deleted.`);
        window.location.reload()
    };

    const updateCart = async () => {
        if (verified === false) {
            setModal(true)
        } else if (selectedQuantity === "0") {
            deleteFromCart()
        } else {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/cart/updatecart?productId=${productId}&quantity=${selectedQuantity}`, { headers });
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
                console.log('NETWORK GOOD');
                console.log(data);
            } catch (error) {
                console.error('Error adding to cart:', error);
            }
            console.log(`Cart updated.`);
            window.location.reload()
        }
    };



    if (loading) {
        return <p>Loading...</p>;
    }

    const toggleModal = () => {
        setModal(!modal)
    }

    const renderProduct = (product) => {
        return (
            <div className="d-flex flex-wrap justify-content-center">
                <Modal isOpen={modal} toggle={toggleModal} style={{ maxWidth: "20rem" }}>
                    <ModalHeader toggle={toggleModal}>
                        Please Log In
                    </ModalHeader>
                    <ModalBody>
                        <div>
                            You must be logged in to use this feature.
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <Button type="" color="login" onClick={() => navigate("/login")}>
                            Log In
                        </Button>
                    </ModalFooter>
                </Modal>
                <Card style={{ maxWidth: '40rem' }}>
                    <CardBody>
                        <CardTitle tag="h2">
                            {product.Name}
                        </CardTitle>
                        <Row>
                            <Col>
                                <CardSubtitle tag="h4">
                                    By {product.Manufacturer}
                                </CardSubtitle>
                            </Col>
                            <Col style={{ textAlign: "right" }}>
                                <CardSubtitle tag="h4">
                                    {'\u2605'}{product.Rating}
                                </CardSubtitle>
                            </Col>
                        </Row>
                        <br></br>
                        <img
                            alt={product.Name}
                            src={product.ImageLocation}
                            width="100%"
                        />
                        <br></br><br></br>
                        <Row className="align-items-center">
                            <Col>
                                <CardSubtitle tag="h2" hidden={product.Sale !== 0}>
                                    ${product.Price}
                                </CardSubtitle>
                                <CardSubtitle tag="h2" hidden={product.Sale === 0}>
                                    <s style={{ color: "darkred" }}>${product.Price}</s> ${product.Price - product.Sale}
                                </CardSubtitle>
                            </Col>
                            <Col xs={"auto"}>
                                <ButtonGroup hidden={cartContains !== 0}>
                                    <Input type="select" style={{ width: 80 }} onChange={e => handleQuantity(e)}>
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
                                    <Button type="submit" style={{ width: 120 }} color="login" onClick={addToCart}>
                                        Add to Cart
                                    </Button>
                                </ButtonGroup>
                                <ButtonGroup hidden={cartContains === 0}>
                                    <Input type="select" style={{ width: 80 }} onChange={e => handleQuantity(e)}>
                                        <option value="" disabled selected hidden>{cartContains}</option>
                                        <option value="0">0</option>
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
                                    <Button type="submit" style={{ width: 120 }} color="login" onClick={updateCart}>
                                        Update Cart
                                    </Button>
                                </ButtonGroup>
                            </Col>
                        </Row>
                        <br></br>
                        <UncontrolledAccordion defaultOpen={['1']} stayOpen>
                            <AccordionItem>
                                <AccordionHeader targetId="1">
                                    Description
                                </AccordionHeader>
                                <AccordionBody accordionId="1">
                                    {product.Description}
                                </AccordionBody>
                            </AccordionItem>
                            <AccordionItem>
                                <AccordionHeader targetId="2">
                                    Details
                                </AccordionHeader>
                                <AccordionBody accordionId="2">
                                    <Table striped>
                                        <tbody>
                                            <tr>
                                                <th>Category</th>
                                                <td>{product.Category} </td>
                                            </tr>
                                            <tr>
                                                <th>Dimensions</th>
                                                <td>{product.Length}" x {product.Width}" x {product.Height}"</td>
                                            </tr>
                                            <tr>
                                                <th>Weight</th>
                                                <td>{product.Weight} lbs.</td>
                                            </tr>
                                            <tr>
                                                <th>SKU</th>
                                                <td>{product.SKU}</td>
                                            </tr>
                                        </tbody>
                                    </Table>
                                </AccordionBody>
                            </AccordionItem>
                        </UncontrolledAccordion>
                    </CardBody>
                </Card>
            </div>
        );
    }

    return (
        renderProduct(product)
    );
}

export default Product;