import React from 'react';
import { Link } from 'react-router-dom';
import { List, ListItem, Typography } from '@material-ui/core';
import Page from '../containers/Page';

function App() {
    return (
        <Page>
            <Typography variant="h6">Production</Typography>
            <Typography variant="h6">Reports</Typography>
            <List>
                <ListItem
                    component={Link}
                    to="/production/maintenance/works-orders/outstanding-works-orders-report"
                    button
                >
                    <Typography color="primary">Outstanding Works Orders Report</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
