import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { DatePicker, Dropdown, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function WhoBuiltWhatReportOptions({ history }) {
    const defaultStartDate = new Date();
    defaultStartDate.setDate(defaultStartDate.getDate() - 7);
    const [fromDate, setFromDate] = useState(defaultStartDate);
    const [toDate, setToDate] = useState(new Date());
    const [citCode, setCitCode] = useState('S');

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/who-built-what/report`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}&citCode=${citCode}`
        });

    return (
        <Page>
            <Title text="Who Built What Report" />
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
                <Grid item xs={6}></Grid>
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

WhoBuiltWhatReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    }).isRequired
};

export default WhoBuiltWhatReportOptions;
