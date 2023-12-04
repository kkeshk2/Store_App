import React, { useEffect, useState } from 'react';
import '../custom.css';
import { Link, useNavigate } from 'react-router-dom';

import {
    Input,
    Label,
    Form,
    Row,
    Col,
    Card,
    CardBody,
    CardImg,
    CardSubtitle
} from 'reactstrap'

function Home() {
    const [products, setProducts] = useState([]);
    const [selection, setSelection] = useState("")
    const [search, setSearch] = useState("")
    const [isChecked, setIsChecked] = useState(false);
    const navigate = useNavigate()

    useEffect(() => {
        const populateProducts = async () => {
            try {
                const response = await fetch('api/product/accessproductlist');
                if (response.status === 404) {
                    navigate("/not-found")
                    window.location.reload()
                } else if (response.status === 500) {
                    navigate("/server-error")
                    window.location.reload()
                }  
                const data = await response.json();
                setProducts(data.Products);

            }
            catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        populateProducts();
    }, [navigate]);

    const handleSelection = (event) => {
        setSelection(event.target.value)
    }

    const handleSearch = (event) => {
        setSearch(event.target.value)
    }

    const handleCheck = () => {
        setIsChecked(!isChecked)
    }

    return (
        <div>
            <div className="d-flex flex-wrap justify-content-center align-items-center">
                <Form style={{ margin: "10px", maxWidth: "40rem", width: "40rem" }}>
                    <Row>
                        <Col xs="3">
                            <Input type="select" size="sm" onChange={e => handleSelection(e)}>
                                <option value="" selected placeholder>Category</option>
                                <option value="Graphics Card">Graphics Cards</option>
                                <option value="Processor">Processors</option>
                                <option value="Cooling">Cooling</option>
                            </Input>

                        </Col>
                        <Col xs="6">
                            <Input placeholder="Search" size="sm" onChange={e => handleSearch(e)}>
                            </Input>
                        </Col>
                        <Col xs="3">
                            <Input id="checkbox" type="checkbox" style={{ marginRight: "1em" }} checked={isChecked} onChange={()=>handleCheck()}>
                                On Sale
                            </Input>
                            <Label for="checkbox">
                                {` `}On Sale
                            </Label>
                        </Col>
                    </Row>
                </Form>
            </div>
            <div className="d-flex flex-wrap justify-content-center">
                {products.map((product) => (
                    <Card style={{ margin: "10px", maxWidth: "24rem", width: "24rem" }}
                        hidden={(selection !== "" && selection.toLowerCase() !== product.Category.toLowerCase()) ||
                            (search !== "" && (!product.Name.toLowerCase().includes(search.toLowerCase()))) ||
                            (isChecked && product.Sale === 0)
                        }
                    >
                        <CardBody>
                            <Link to={`/product/${product.ProductId}`} style={{ color: "inherit", textDecoration: "none" }}>
                            <CardSubtitle style={{ fontSize: 20, fontWeight: 600 }}>
                                {product.Name}
                            </CardSubtitle>
                            </Link>
                            <Row>
                                <Col>
                                    <CardSubtitle tag="h6">
                                        By {product.Manufacturer}
                                    </CardSubtitle>
                                </Col>
                                <Col style={{ textAlign: "right" }}>
                                    <CardSubtitle tag="h6">
                                        {'\u2605'}{product.Rating}
                                    </CardSubtitle>
                                </Col>
                            </Row>
                            <br></br>
                            <Link to={`/product/${product.ProductId}`} style={{ color: "inherit", textDecoration: "none" }}>
                            <CardImg
                                alt={product.Name}
                                src={product.ImageLocation}
                                height="62.5%"
                            />
                            </Link>
                            <br></br><br></br>
                            <Row>
                                <Col>
                                    <CardSubtitle tag="h4" hidden={product.Sale !== 0}>
                                        ${product.Price}
                                    </CardSubtitle>
                                    <CardSubtitle tag="h4" hidden={product.Sale === 0}>
                                        <s style={{ color: "darkred" }}>${product.Price}</s> ${product.Price - product.Sale}
                                    </CardSubtitle>
                                </Col>
                            </Row>
                        </CardBody>
                    </Card>
                ))}
            </div>
        </div>
    );
}

export default Home;
