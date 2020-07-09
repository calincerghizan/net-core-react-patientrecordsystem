import React, { Component } from 'react';
import ReactTable from 'react-table-6';
import 'react-table-6/react-table.css';
import { ListGroup } from 'react-bootstrap';

export class MetaReport extends Component {

    constructor(props) {
        super(props);
        this.state = {
            metaReport: ''
        }
    }

    componentDidMount() {
        const url = "/api/report/meta";
        fetch(url,
            {
                method: "GET"
            }).then(response => response.json()).then(metaReport => {
                console.log(metaReport);
                this.setState({ metaReport: metaReport });
        });
    }

    render() {

        const columns = [
            {
                Header: 'Key',
                accessor: 'key'
            },
            {
                Header: 'Occurrence',
                accessor: 'occurrence'
            }
        ];

        return (
            <div>
            <h3>MetaData Report</h3> 
            <ListGroup>
                    <ListGroup.Item>
                        Average number of meta-data used <h5>{this.state.metaReport.metaUsedAverage}</h5>
                    </ListGroup.Item>
                    <ListGroup.Item>
                        Maximum number of meta-data used <h5>{this.state.metaReport.metaUsedMax}</h5>
                    </ListGroup.Item> 
                    <ListGroup.Item>
                        Top 3 highest keys used
                        <ReactTable data={this.state.metaReport.topThreeUsedKeys} columns={columns} defaultPageSize={3} />
                    </ListGroup.Item> 
            </ListGroup> 
            </div>
    )
    }
}