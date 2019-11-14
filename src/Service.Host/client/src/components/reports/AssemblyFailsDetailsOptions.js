import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { DatePicker, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AssemblyFailsDetailsOptions({ history, prevOptions }) {
    const defaultStartDate = new Date();
    defaultStartDate.setDate(defaultStartDate.getDate() - 90);
    const [fromDate, setFromDate] = useState(
        prevOptions.fromDate ? new Date(prevOptions.fromDate) : defaultStartDate
    );
    const [toDate, setToDate] = useState(
        prevOptions.toDate ? new Date(prevOptions.toDate) : new Date()
    );

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/assembly-fails-details/report`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}`
        });

    return (
        <Page>
            <Title text="Assembly Fails Details" />
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
                <Grid item xs={6} />
                <Grid item xs={12}>
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

AssemblyFailsDetailsOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    }).isRequired
};

export default AssemblyFailsDetailsOptions;
