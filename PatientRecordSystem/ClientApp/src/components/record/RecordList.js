import React, { Component } from 'react';
import ReactTable from 'react-table-6';
import 'react-table-6/react-table.css';
import { Button } from 'react-bootstrap';

export class RecordList extends Component {
    static displayName = RecordList.name;

    constructor(props) {
        super(props);
        this.onEditClick = this.onEditClick.bind(this);
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

    onEditClick(record) {
        const location = {
            pathname: '/edit-record/' + record.id
        };
        this.props.history.push(location);
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
                Cell: ({ row }) => {
                    return (
                        <Button variant="outline-primary" size="sm" onClick={(e) => this.onEditClick(row._original)}>Edit</Button>)
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