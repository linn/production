import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { DatePicker, Title, LinnWeekPicker } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import * as moment from 'moment';
import Page from '../../containers/Page';

function TimingsSetup({ history }) {
    const [fromDate, setFromDate] = useState(new Date());
    const [toDate, setToDate] = useState(new Date());

    const handleDateChange = (propertyName, newValue) => {
        if (propertyName === 'week') {
            setFromDate(newValue);
            console.info(newValue);
            // let utc = moment(newValue).add(6, 'd');
            // console.info(utc);
            //todo - get adding days onto moment working. Ask adam maybe?
            setToDate(newValue.add(6, 'd'));
        }
    };
    const handleClick = () =>
        history.push({
            pathname: `/production/reports/builds-summary`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}`
        });

    return (
        <Page>
            <Title text="Metalwork Timings Setup" />
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={3}>
                    <Typography variant="h6" gutterBottom>
                        Select Linn Week:
                    </Typography>
                </Grid>
                <Grid item xs={3}>
                    <LinnWeekPicker
                        label="Week beginning"
                        selectedDate={fromDate.toString()}
                        setWeekStartDate={handleDateChange}
                        propertyName="week"
                    />
                </Grid>
                <Grid item xs={3} />
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
                <Grid item xs={12} />
                <Grid item xs={3}>
                    <Typography variant="h6" gutterBottom>
                        Or select a different date range:
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
                <Grid item xs={3} />
            </Grid>
        </Page>
    );
}

TimingsSetup.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    }).isRequired
};

export default TimingsSetup;
