import React, { useState, useEffect } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import {
    Dropdown,
    Title,
    Loading,
    InputField,
    TypeaheadDialog
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';
import LinnWeekDatePicker from '../LinnWeekDatePicker';

const useStyles = makeStyles(theme => ({
    marginTop: {
        marginTop: theme.spacing(2)
    }
}));

const getWeekStartDate = date => {
    if (date.day() === 6) {
        return date.clone().endOf('week');
    }
    return date
        .clone()
        .startOf('week')
        .subtract(1, 'days');
};

export default function PartFailDetailsReportOptions({
    history,
    prevOptions,
    partFailErrorTypes,
    partFailErrorTypesLoading,
    partFailFaultCodes,
    partFailFaultCodesLoading,
    partsSearchLoading,
    partsSearchResults,
    departments,
    departmentsLoading,
    searchParts,
    clearPartsSearch,
    suppliers,
    suppliersLoading
}) {
    const [errorTypeOptions, setErrorTypes] = useState(['All']);
    const [faultCodeOptions, setFaultCodes] = useState(['All']);
    const [departmentOptions, setDepartmentList] = useState(['All']);
    const [suppliersOptions, setSuppliers] = useState(['All']);
    // TODO get appropriate from and end week defaults
    const [reportOptions, setReportOptions] = useState({
        supplierId: 'All',
        fromWeek: getWeekStartDate(moment().subtract(1, 'month')),
        toWeek: getWeekStartDate(moment()),
        errorType: 'All',
        faultCode: 'All',
        partNumber: 'All',
        partDescription: '',
        department: 'All'
    });

    const classes = useStyles();

    useEffect(() => {
        const list = ['All'];
        if (partFailErrorTypes) {
            setErrorTypes([...list, ...partFailErrorTypes.map(errorType => errorType.errorType)]);
            return;
        }
        setErrorTypes(list);
    }, [partFailErrorTypes]);

    useEffect(() => {
        const list = [{ id: 'All', displayText: 'All' }];
        if (partFailFaultCodes) {
            setFaultCodes([
                ...list,
                ...partFailFaultCodes.map(code => ({
                    id: code.faultCode,
                    displayText: code.faultDescription
                }))
            ]);
            return;
        }
        setFaultCodes(list);
    }, [partFailFaultCodes]);

    useEffect(() => {
        const list = [{ id: 'All', displayText: 'All' }];
        if (departments) {
            setDepartmentList([
                ...list,
                ...departments.map(department => ({
                    id: department.departmentCode,
                    displayText: department.description
                }))
            ]);
            return;
        }
        setDepartmentList(list);
    }, [departments]);

    useEffect(() => {
        const list = [{ id: 'All', displayText: 'All' }];
        if (suppliers) {
            setSuppliers([
                ...list,
                ...suppliers.map(supplier => ({
                    id: supplier.supplierId,
                    displayText: supplier.supplierName
                }))
            ]);
            return;
        }
        setSuppliers(list);
    }, [suppliers]);

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'department' || propertyName === 'faultCode') {
            setReportOptions({
                ...reportOptions,
                [propertyName]: newValue.id
            });
            return;
        }
        setReportOptions({
            ...reportOptions,
            [propertyName]: newValue
        });
    };

    const handlePartSelect = part => {
        setReportOptions({
            ...reportOptions,
            partNumber: part.partNumber,
            partDescription: part.description
        });
    };

    const handleRunClick = () => {
        const fromString = reportOptions.fromWeek.toISOString();
        const toString = reportOptions.toWeek.toISOString();

        const searchString = `?errorType=${reportOptions.errorType}&fromWeek=${fromString}&toWeek=${toString}&faultCode=${reportOptions.faultCode}&partNumber=${reportOptions.partNumber}&department=${reportOptions.department}`;

        history.push({
            pathname: '/production/quality/part-fails/detail-report/report',
            search: `${
                reportOptions.supplierId === 'All'
                    ? searchString
                    : `${searchString}&supplierId=${reportOptions.supplierId}`
            }`
        });
    };

    return (
        <Page>
            <Title text="Part Fail Details Report" />
            {partFailErrorTypesLoading ||
            partFailFaultCodesLoading ||
            departmentsLoading ||
            suppliersLoading ? (
                <Loading />
            ) : (
                <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                    <Grid item xs={4}>
                        <LinnWeekDatePicker
                            weekStartDate={reportOptions.fromWeek}
                            setWeekStartDate={handleFieldChange}
                            propertyName="fromWeek"
                            label="From Week Starting"
                            required={false}
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={4}>
                        <LinnWeekDatePicker
                            weekStartDate={reportOptions.toWeek}
                            setWeekStartDate={handleFieldChange}
                            propertyName="toWeek"
                            label="To Week Starting"
                            required={false}
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={4}>
                        <Dropdown
                            label="Supplier"
                            items={suppliersOptions}
                            fullWidth
                            value={reportOptions.supplierId}
                            onChange={handleFieldChange}
                            propertyName="supplierId"
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={4}>
                        <Dropdown
                            label="Error Type"
                            items={errorTypeOptions}
                            fullWidth
                            value={reportOptions.errorType}
                            onChange={handleFieldChange}
                            propertyName="errorType"
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={4}>
                        <Dropdown
                            label="Fault Code"
                            items={faultCodeOptions}
                            fullWidth
                            value={reportOptions.faultCode}
                            onChange={handleFieldChange}
                            propertyName="faultCode"
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={4}>
                        <InputField
                            disabled
                            label="Part"
                            maxLength={14}
                            fullWidth
                            value={reportOptions.partNumber}
                            onChange={handleFieldChange}
                            propertyName="partSearchTerm"
                        />
                    </Grid>
                    <Grid item xs={1}>
                        <div className={classes.marginTop}>
                            <TypeaheadDialog
                                title="Search For Part"
                                onSelect={handlePartSelect}
                                searchItems={partsSearchResults || []}
                                loading={partsSearchLoading}
                                fetchItems={searchParts}
                                clearSearch={() => clearPartsSearch}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={7}>
                        <InputField
                            fullWidth
                            disabled
                            value={reportOptions.partDescription}
                            label="Description"
                        />
                    </Grid>
                    <Grid item xs={4}>
                        <Dropdown
                            label="Department"
                            items={departmentOptions}
                            fullWidth
                            value={reportOptions.department}
                            onChange={handleFieldChange}
                            propertyName="department"
                        />
                    </Grid>
                    <Grid item xs={8} />
                    <Grid item xs={12}>
                        <Button
                            color="primary"
                            variant="contained"
                            style={{ float: 'right' }}
                            onClick={handleRunClick}
                        >
                            Run Report
                        </Button>
                    </Grid>
                </Grid>
            )}
        </Page>
    );
}
