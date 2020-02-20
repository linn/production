import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { OnOffSwitch, DatePicker } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function BuildsByDepartmentReportOptions({ history }) {
    const [fromDate, setFromDate] = useState(new Date().toISOString());
    const [toDate, setToDate] = useState(new Date().toISOString());
    const [monthly, setMonthly] = useState(false);

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/builds-summary`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}&monthly=${monthly}`
        });

    return (
        <Page>
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Typography variant="h6" gutterBottom>
                        Choose a date range:
                    </Typography>
                </Grid>
                <Grid item xs={3}>
                    <DatePicker label="From Date" value={fromDate} onChange={setFromDate} />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="To Date"
                        value={toDate}
                        minDate={fromDate}
                        onChange={setToDate}
                    />
                </Grid>
                <Grid item xs={3}>
                    <OnOffSwitch
                        label="Total by months"
                        value={monthly}
                        onChange={() => setMonthly(!monthly)}
                        propertyName="monthly"
                    />
                </Grid>
                <Grid item xs={3}>
                    <Button
                        color="primary"
                        variant="contained"
                        disabled={!fromDate && !toDate}
                        onClick={handleClick}
                    >
                        Run Report
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

BuildsByDepartmentReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    })
};

BuildsByDepartmentReportOptions.defaultProps = {
    prevOptions: null
};

export default BuildsByDepartmentReportOptions;
