import React, { Component } from 'react';
import ReactTable from 'react-table-6';
import 'react-table-6/react-table.css';
import { Button } from 'react-bootstrap';

export class RecordList extends Component {
    static displayName = RecordList.name;

    constructor(props) {
        super(props);
        this.state = {
            records: []
        }
    }

    componentDidMount() {
        const url = "/api/record";
        fetch(url,
            {
                method: "GET"
            }).then(response => response.json()).then(records => {
            this.setState({ records: records });
        });
    }

    render() {
        const columns = [
            {
                Header: 'Patient name',
                filterable: true,
                accessor: 'patientName'
            },
            {
                Header: 'Disease name',
                filterable: true,
                accessor: 'diseaseName'
            },
            {
                Header: 'Time of entry',
                filterable: false,
                accessor: 'timeOfEntry'
            },
            {
                Header: '',
                Cell: props => {
                    return (
                    <Button variant="outline-primary" size="sm">Edit</Button>)
                },
                width: 100
            }
        ];

        return (
            <div>
            <h3>Record List</h3>
            <ReactTable data={this.state.records} columns={columns} defaultPageSize={5} />
            </div>
    )
}
}