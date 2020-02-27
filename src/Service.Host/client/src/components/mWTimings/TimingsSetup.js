import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import {
    DatePicker,
    Title,
    LinnWeekPicker,
    getWeekEndDate,
    Dropdown
} from '@linn-it/linn-form-components-library/cjs/';
import { makeStyles } from '@material-ui/styles';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    topMargin: {
        marginTop: theme.spacing(4)
    },
    bigTopMargin: {
        marginTop: theme.spacing(8)
    }
}));

function TimingsSetup({ history, cits, citsLoading }) {
    const classes = useStyles();
    const [fromDate, setFromDate] = useState(new Date());
    const [toDate, setToDate] = useState(new Date());
    const [selectedCit, setSelectedCit] = useState('K');

    const handleWeekChange = (propertyName, newValue) => {
        setFromDate(newValue);
        setToDate(getWeekEndDate(newValue));
    };
    const handleClick = () =>
        history.push({
            pathname: `/production/reports/mw-timings`,
            search: `?startDate=${fromDate.toISOString()}&endDate=${toDate.toISOString()}&citCode=${selectedCit}`
        });

    const handleCitChange = (propertyName, newValue) => {
        setSelectedCit(newValue);
    };

    return (
        <Page>
            <Title text="Metalwork Timings Setup" />
            <Grid className={classes.topMargin} container spacing={3} justify="center">
                <Grid item xs={3}>
                    <Typography variant="h6" gutterBottom>
                        Select Linn Week:
                    </Typography>
                </Grid>
                <Grid item xs={3}>
                    <LinnWeekPicker
                        label="Week beginning"
                        selectedDate={fromDate.toString()}
                        setWeekStartDate={handleWeekChange}
                        propertyName="week"
                        required
                    />
                </Grid>
                <Grid item xs={3}>
                    <Dropdown
                        label="CitCode"
                        propertyName="cit"
                        items={cits.map(s => ({
                            id: s.code,
                            displayText: `${s.code} (${s.name})`
                        }))}
                        fullWidth
                        value={selectedCit}
                        onChange={handleCitChange}
                        optionsLoading={citsLoading}
                    />
                </Grid>
                <Grid item xs={1} />
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
                <Grid className={classes.bigTopMargin} item xs={12} />
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
    }).isRequired,
    citsLoading: PropTypes.bool,
    cits: PropTypes.arrayOf(PropTypes.shape({ name: PropTypes.string, code: PropTypes.string }))
};

TimingsSetup.defaultProps = {
    citsLoading: false,
    cits: null
};

export default TimingsSetup;
