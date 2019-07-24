import React, { useState } from 'react';
import { Button, Grid, Typography } from '@material-ui/core';
import { DatePicker } from '@material-ui/pickers';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function BuildsByDepartmentReportOptions({ history }) {
    const [fromDate, setFromDate] = useState(new Date('2006-01-27'));
    const [toDate, setToDate] = useState(new Date('2006-01-28'));

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/builds-summary`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}`
        });
    return (
        <Page>
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Typography variant="h6" gutterBottom>
                        Choose a date range:
                    </Typography>
                </Grid>
                <Grid item xs={5}>
                    <DatePicker label="From Date" value={fromDate} onChange={setFromDate} />
                </Grid>
                <Grid item xs={5}>
                    <DatePicker
                        label="To Date"
                        value={toDate}
                        minDate={fromDate}
                        onChange={setToDate}
                    />
                </Grid>
                <Grid item xs={2}>
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
    }).isRequired
};

export default BuildsByDepartmentReportOptions;
