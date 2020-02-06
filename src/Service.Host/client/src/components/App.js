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
                <ListItem component={Link} to="/production/quality/assembly-fails" button>
                    <Typography color="primary">Assembly Fails Utility</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/quality/assembly-fail-fault-codes"
                    button
                >
                    <Typography color="primary">Assembly Fail Fault Codes Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/quality/part-fails" button>
                    <Typography color="primary">Part Fail Log</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/quality/part-fail-fault-codes" button>
                    <Typography color="primary">Part Fail Fault Codes</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/quality/part-fail-error-types" button>
                    <Typography color="primary">Part Fail Error Types</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/resources/board-fail-types/" button>
                    <Typography color="primary">Board Fail Types</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/maintenance/labels/reprint" button>
                    <Typography color="primary">Label Reprint</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/resources/label-types/" button>
                    <Typography color="primary">Label Types Utility</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/maintenance/labels/reprint-reasons/create"
                    button
                >
                    <Typography color="primary">Label Reprint Reissue Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/maintenance/labels/print" button>
                    <Typography color="primary">General Purpose Label Printer</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/resources/manufacturing-resources/"
                    button
                >
                    <Typography color="primary">Manufacturing Resources Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/resources/manufacturing-routes/" button>
                    <Typography color="primary">Manufacturing Routes Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/resources/manufacturing-skills/" button>
                    <Typography color="primary">Manufacturing Skills Utility</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/maintenance/serial-number-reissue"
                    button
                >
                    <Typography color="primary">Reissue Serial Numbers</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/works-orders" button>
                    <Typography color="primary">Works Orders Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/works-orders/labels" button>
                    <Typography color="primary">Works Orders Labels Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/works-orders/batch-notes" button>
                    <Typography color="primary">Works Orders Batch Notes</Typography>
                </ListItem>
            </List>
            <Typography variant="h6">Reports</Typography>
            <List>
                <ListItem component={Link} to="/production/reports/assembly-fails-measures" button>
                    <Typography color="primary">Assembly Fails Measures</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/assembly-fails-details" button>
                    <Typography color="primary">Assembly Fails Details Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/ate/status" button>
                    <Typography color="primary">ATE Status Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/board-tests-report" button>
                    <Typography color="primary">Board Tests Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/builds-detail/options" button>
                    <Typography color="primary">Builds Detail Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/build-plans" button>
                    <Typography color="primary">Build Plan Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/builds-summary/options" button>
                    <Typography color="primary">Builds Summary Report</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/reports/manufacturing-commit-date"
                    button
                >
                    <Typography color="primary">Manufacturing Commit Date Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/measures" button>
                    <Typography color="primary">Operations Status Report (OSR)</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/works-orders/outstanding-works-orders-report"
                    button
                >
                    <Typography color="primary">Outstanding Works Orders Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/quality/part-fails/detail-report" button>
                    <Typography color="primary">Part Fail Details Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/production-back-orders" button>
                    <Typography color="primary">Production Back Orders Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/overdue-orders" button>
                    <Typography color="primary">Overdue Orders Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/triggers" button>
                    <Typography color="primary">Production Triggers Report</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/maintenance/production-trigger-levels"
                    button
                >
                    <Typography color="primary">Production Trigger Levels Utility</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/maintenance/production-trigger-levels-settings"
                    button
                >
                    <Typography color="primary">Production Trigger Level Settings</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/production/reports/smt/outstanding-works-order-parts"
                    button
                >
                    <Typography color="primary">SMT Outstanding Works Order Parts</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/who-built-what" button>
                    <Typography color="primary">Who Built What Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/production/reports/failed-parts" button>
                    <Typography color="primary">Failed Parts Report</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
