import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Dropdown, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function OverdueOrdersReportOptions({ history }) {
    const [reportBy, setReportBy] = useState('Requested Delivery Date');
    const [daysMethod, setDaysMethod] = useState('Working Days');

    const reportByOptions = ['Requested Delivery Date', 'First Advised Date'];
    const daysMethodOptions = ['Working Days', 'Actual Days'];

    const handleRunClick = () => {
        history.push({
            pathname: '/production/reports/overdue-orders/report',
            search: `?reportBy=${reportBy}&daysMethod=${daysMethod}`
        });
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'reportBy') {
            setReportBy(newValue);
            return;
        }
        setDaysMethod(newValue);
    };

    return (
        <Page>
            <Title text="Overdue Orders Report" />
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={4}>
                    <Dropdown
                        label="Report By"
                        items={reportByOptions}
                        fullWidth
                        value={reportBy}
                        onChange={handleFieldChange}
                        propertyName="reportBy"
                    />
                </Grid>
                <Grid item xs={8} />
                <Grid item xs={4}>
                    <Dropdown
                        label="Days Method"
                        items={daysMethodOptions}
                        fullWidth
                        value={daysMethod}
                        onChange={handleFieldChange}
                        propertyName="daysMethod"
                    />
                </Grid>
                <Grid item xs={8} />
                <Grid item xs={12}>
                    <Button color="primary" variant="contained" onClick={handleRunClick}>
                        Run Report
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

OverdueOrdersReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired
};
