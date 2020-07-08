import React, { Component } from 'react';
import { ListGroup, Badge  } from 'react-bootstrap';

export class MetaReport extends Component {

    constructor(props) {
        super(props);
        this.state = {
            metaReport: '',
            first: '',
            second: '',
            third: ''
        }
    }

    componentDidMount() {
        const url = "/api/report/meta";
        fetch(url,
            {
                method: "GET"
            }).then(response => response.json()).then(metaReport => {
                const first = metaReport.topThreeUsedKeys[0];
                const second = metaReport.topThreeUsedKeys[1];
                const third = metaReport.topThreeUsedKeys[2];
                this.setState({ metaReport: metaReport, first: first, second: second, third: third });
        });
    }

    render() {
        return (
            <div>
            <h3>MetaData Report</h3> 
            <ListGroup>
                    <ListGroup.Item>
                        Average number of meta-data used <Badge pill variant="primary">{this.state.metaReport.metaUsedAverage}</Badge>
                    </ListGroup.Item>
                    <ListGroup.Item>
                        Maximum number of meta-data used <Badge pill variant="primary">{this.state.metaReport.metaUsedMax}</Badge>
                    </ListGroup.Item> 
                    <ListGroup.Item>
                        Top 3 highest keys used
                        <ListGroup>
                            <ListGroup.Item>
                                Key <Badge variant="success">{this.state.first.key}</Badge> has an occurrence of <Badge pill variant="primary">{this.state.first.occurrence}</Badge>
                            </ListGroup.Item> 
                            <ListGroup.Item>
                                Key <Badge variant="success">{this.state.second.key}</Badge> has an occurrence of <Badge pill variant="primary">{this.state.second.occurrence}</Badge>
                            </ListGroup.Item> 
                            <ListGroup.Item>
                                Key <Badge variant="success">{this.state.third.key}</Badge> has an occurrence of <Badge pill variant="primary">{this.state.third.occurrence}</Badge>
                            </ListGroup.Item> 
                        </ListGroup> 
                    </ListGroup.Item> 
                </ListGroup> 
            </div>
    )
    }
}