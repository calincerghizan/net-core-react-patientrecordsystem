import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
      return (
          <div>
            <h3>Welcome to Patient Record System</h3> 
            <p><strong>You can do the followings:</strong></p>
            <ul>
                <li>Add/Edit a patient</li>
                <li>List all the patients</li>
                <li>Present a report for every patient</li>
                <li>Present a meta report</li> 
                <li>Add/Edit a record for a patient</li>
                <li>List all the records</li>
            </ul>
        </div>
  );
  }
}