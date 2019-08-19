import React from 'react';
import { Link } from 'react-router-dom';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Typography from '@material-ui/core/Typography';
import Page from '../containers/Page';

function App() {
    return (
        <Page>
            <Typography variant="h6">Production</Typography>
            <List>
                <ListItem component={Link} to="/production/quality/ate/fault-codes/" button>
                    <Typography color="primary">ATE Fault Codes</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/maintenance/serial-number-reissue"
                    button
                >
                    <Typography color="primary">Reissue Serial Numbers</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/resources/manufacturing-skills/" button>
                    <Typography color="primary">Manufacturing Routes Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/resources/board-fail-types/" button>
                    <Typography color="primary">Board Fail Types</Typography>
                </ListItem>
            </List>
            <Typography variant="h6">Reports</Typography>
            <List>
                <ListItem component={Link} to="/production/reports/builds-summary-options" button>
                    <Typography color="primary">Builds Sumary Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/builds-detail-options" button>
                    <Typography color="primary">Builds Detail Report</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/maintenance/works-orders/outstanding-works-orders-report"
                    button
                >
                    <Typography color="primary">Outstanding Works Orders Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/measures" button>
                    <Typography color="primary">Operations Status Report (OSR)</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
