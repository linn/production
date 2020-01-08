import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { DatePicker, Title, Dropdown } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AteStatusReportOptions({ history, prevOptions }) {
    const defaultStartDate = new Date();
    function nextWeekdayDate(date, dayInWeek) {
        const ret = new Date(date || new Date());
        ret.setDate(ret.getDate() + ((dayInWeek - 1 - ret.getDay() + 7) % 7) + 1);
        return ret;
    }

    defaultStartDate.setDate(nextWeekdayDate(defaultStartDate, 6).getDate() - 28);
    const [fromDate, setFromDate] = useState(
        prevOptions.fromDate ? new Date(prevOptions.fromDate) : defaultStartDate
    );
    const [toDate, setToDate] = useState(
        prevOptions.toDate ? new Date(prevOptions.toDate) : new Date()
    );
    const [groupBy, setGroupBy] = useState(prevOptions.groupBy ? prevOptions.groupBy : 'board');
    const [smtOrPcb, setSmtOrPcb] = useState(prevOptions.smtOrPcb ? prevOptions.smtOrPcb : 'SMT');
    const [placeFound, setPlaceFound] = useState(
        prevOptions.placeFound ? prevOptions.placeFound : 'ATE'
    );

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/ate/status/report`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}&groupBy=${groupBy}&smtOrPcb=${smtOrPcb}&placeFound=${placeFound}`
        });

    const handleOptionChange = (field, value) => {
        switch (field) {
            case 'groupBy':
                setGroupBy(value);
                break;
            case 'placeFound':
                setPlaceFound(value);
                break;
            case 'smtOrPcb':
                setSmtOrPcb(value);
                break;
            default:
        }
    };

    return (
        <Page width="s">
            <Title text="ATE Status Report" />
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Typography variant="h6" gutterBottom>
                        Choose a date range:
                    </Typography>
                </Grid>
                <Grid item xs={4}>
                    <DatePicker
                        label="From Date"
                        value={fromDate.toString()}
                        onChange={setFromDate}
                    />
                </Grid>
                <Grid item xs={4}>
                    <DatePicker
                        label="To Date"
                        value={toDate.toString()}
                        minDate={fromDate.toString()}
                        onChange={setToDate}
                    />
                </Grid>
                <Grid item xs={4} />
                <Grid item xs={3}>
                    <Dropdown
                        label="Group By"
                        propertyName="groupBy"
                        allowNoValue={false}
                        items={[
                            { id: 'board', displayText: 'Board' },
                            { id: 'component', displayText: 'Component' },
                            { id: 'fault-code', displayText: 'Fault Code' }
                        ]}
                        value={groupBy}
                        onChange={handleOptionChange}
                    />
                </Grid>
                <Grid item xs={9} />
                <Grid item xs={3}>
                    <Dropdown
                        label="SMT or PCB"
                        propertyName="smtOrPcb"
                        allowNoValue
                        items={[
                            { id: 'SMT', displayText: 'SMT' },
                            { id: 'PCB', displayText: 'PCB' }
                        ]}
                        value={smtOrPcb}
                        onChange={handleOptionChange}
                    />
                </Grid>
                <Grid item xs={9} />
                <Grid item xs={3}>
                    <Dropdown
                        label="Place Found"
                        propertyName="placeFound"
                        allowNoValue
                        items={[
                            { id: 'ATE', displayText: 'ATE' },
                            { id: 'SMT', displayText: 'SMT' }
                        ]}
                        value={placeFound}
                        onChange={handleOptionChange}
                    />
                </Grid>
                <Grid item xs={9} />
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

AteStatusReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    }).isRequired
};

export default AteStatusReportOptions;
