import React, { useState, useEffect } from 'react';
import { Button, Grid, Typography } from '@material-ui/core';
import { OnOffSwitch, Dropdown } from '@linn-it/linn-form-components-library';
import { KeyboardDatePicker } from '@material-ui/pickers';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';
import DatePicker from './DatePicker';

function BuildsDetailReportOptions({ history, departments, options, prevOptions }) {
    const getFromDate = () => {
        if (options.fromDate) {
            return moment(options.fromDate.replace(' ', '+'));
        }
        if (prevOptions) {
            return moment(prevOptions.fromDate);
        }
        return moment();
    };

    const getToDate = () => {
        if (options.toDate) {
            return moment(options.toDate.replace(' ', '+'));
        }
        if (prevOptions.toDate) {
            return moment(prevOptions.toDate);
        }
        return moment();
    };

    const getMonthly = () => {
        if (options.monthly) {
            return options.monthly === 'true';
        }
        if (prevOptions.monthly) {
            return prevOptions.monthly === 'true';
        }
        return false;
    };

    const getQuantityOrValue = () => {
        if (options.quantityOrValue) {
            return options.quantityOrValue;
        }
        if (prevOptions.quantityOrValue) {
            return prevOptions.quantityOrValue;
        }
        return 'Value';
    };

    const [fromDate, setFromDate] = useState(getFromDate());
    const [toDate, setToDate] = useState(getToDate());
    const [monthly, setMonthly] = useState(getMonthly());
    const [department, setDepartment] = useState({ departmentCode: null, description: 'loading' });
    const [quantityOrValue, setQuantityOrValue] = useState(getQuantityOrValue());

    useEffect(() => {
        if (departments && options.department) {
            setDepartment(departments.find(d => d.departmentCode === options.department));
        } else if (departments && prevOptions.department) {
            setDepartment(departments.find(d => d.departmentCode === prevOptions.department));
        } else if (departments) {
            setDepartment(departments[0]);
        }
    }, [departments, options, prevOptions]);

    const handleDepartmentChange = (propertyName, newValue) => {
        setDepartment(departments.find(d => d.description === newValue));
    };

    const handleQuantityOrValueChange = (propertyName, newValue) => {
        setQuantityOrValue(newValue);
    };

    const handleClick = () => {
        history.push({
            pathname: `/production/reports/builds-detail`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}&monthly=${monthly}&department=${
                department.departmentCode
            }&quantityOrValue=${quantityOrValue}`
        });
    };

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
                        items={departments.map(d => d.description)}
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
                    <KeyboardDatePicker
                        allowKeyboardControl
                        autoOk
                        label="From Date"
                        value={fromDate}
                        onChange={setFromDate}
                    />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        value={toDate}
                        label="To Date"
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
                        disabled={!fromDate && !toDate && department.departmentCode}
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
    options: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string,
        departmentCode: PropTypes.string,
        quantityOrValue: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    departments: PropTypes.arrayOf(
        PropTypes.shape({ departmentCode: PropTypes.string, description: PropTypes.string })
    ),
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    })
};
BuildsDetailReportOptions.defaultProps = {
    departments: [],
    options: null,
    prevOptions: null
};

export default BuildsDetailReportOptions;
