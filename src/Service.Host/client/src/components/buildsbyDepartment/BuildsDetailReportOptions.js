import React, { useState, useEffect } from 'react';
import { Button, Grid, Typography } from '@material-ui/core';
import { OnOffSwitch, Dropdown } from '@linn-it/linn-form-components-library';
import { DatePicker } from '@material-ui/pickers';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function BuildsDetailReportOptions({ history, departments, departmentsLoading, errorMessage }) {
    const [fromDate, setFromDate] = useState(new Date());
    const [toDate, setToDate] = useState(new Date());
    const [monthly, setMonthly] = useState(false);
    const [department, setDepartment] = useState({ departmentCode: '', description: '' });
    const [quantityOrValue, setQuantityOrValue] = useState('Value');

    useEffect(() => {
        setDepartment(departments[0]);
    }, [departments]);

    const handleDepartmentChange = (propertyName, newValue) => {
        setDepartment(departments.find(d => d.description === newValue));
    };

    const handleQuantityOrValueChange = (propertyName, newValue) => {
        setQuantityOrValue(newValue);
    };

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/builds-detail`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}&monthly=${monthly}&department=${
                department.departmentCode
            }&quantityOrValue=${quantityOrValue}`
        });

    return (
        <Page>
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Typography variant="h6" gutterBottom>
                        Report Options:
                    </Typography>
                </Grid>
                <Grid item xs={6}>
                    <Dropdown
                        label="Department"
                        propertyName="department"
                        items={[...departments.map(d => d.description)]}
                        fullWidth
                        value={department ? department.description : ''}
                        onChange={handleDepartmentChange}
                    />
                </Grid>
                <Grid item xs={6}>
                    <Dropdown
                        label="Quantity Or Value"
                        propertyName="quantityOrValue"
                        items={['Value', 'Quantity', 'Mins']}
                        fullWidth
                        value={quantityOrValue}
                        onChange={handleQuantityOrValueChange}
                    />
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
                        style={{ float: 'right' }}
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

BuildsDetailReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    }).isRequired
};

export default BuildsDetailReportOptions;
