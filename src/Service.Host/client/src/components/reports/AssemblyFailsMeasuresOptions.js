import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { DatePicker, Title, Dropdown } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AssemblyFailsMeasuresOptions({ history, prevOptions }) {
    const defaultStartDate = new Date();
    defaultStartDate.setDate(defaultStartDate.getDate() - 90);
    const [fromDate, setFromDate] = useState(
        prevOptions.fromDate ? new Date(prevOptions.fromDate) : defaultStartDate
    );
    const [toDate, setToDate] = useState(
        prevOptions.toDate ? new Date(prevOptions.toDate) : new Date()
    );
    const [groupBy, setGroupBy] = useState(
        prevOptions.groupBy ? prevOptions.groupBy : 'board-part-number'
    );

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/assembly-fails-measures/report`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}&groupBy=${groupBy}`
        });

    const handleGroupByChange = (_field, value) => {
        setGroupBy(value);
    };

    return (
        <Page>
            <Title text="Assembly Fails Measures" />
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
                <Grid item xs={3}>
                    <Dropdown
                        label="Group By"
                        propertyName="groupBy"
                        items={[
                            { id: 'board-part-number', displayText: 'Board Part Number' },
                            { id: 'board', displayText: 'Board' },
                            { id: 'fault', displayText: 'Fault' },
                            { id: 'circuit-part-number', displayText: 'Circuit Part Number' },
                            { id: 'cit', displayText: 'Cit' },
                            { id: 'person', displayText: 'Person Responsible' }
                        ]}
                        value={groupBy}
                        onChange={handleGroupByChange}
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

AssemblyFailsMeasuresOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    }).isRequired
};

export default AssemblyFailsMeasuresOptions;
