import React, { Fragment, useState, useEffect } from 'react';
import {
    ReportTable,
    Loading,
    Title,
    ExportButton,
    ErrorCard,
    Dropdown,
    InputField
} from '@linn-it/linn-form-components-library';
import { Button, Grid, Typography } from '@material-ui/core';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function OutstandingWorksOrdersReportOptions({ cits, citsLoading, history }) {
    const [options, setOptions] = useState({ reportType: 'All', cit: '' });
    const [citOptions, setCitOptions] = useState([]);

    useEffect(() => {
        if (cits !== null) {
            const citsFormatted = cits.map(cit => ({
                id: cit.code,
                displayText: cit.name
            }));

            setCitOptions([{ id: '', displayText: '' }, ...citsFormatted]);
        }
    }, [cits]);

    const filterOptions = ['All', 'CIT'];

    const handleClick = () => {
        const queryString =
            options.reportType === 'All'
                ? ''
                : `?reportType=${options.reportType}&searchParameter=${options.cit}`;

        history.push({
            pathname: '/production/maintenance/works-orders/outstanding-works-orders-report/report',
            search: queryString
        });
    };

    const handleFieldChange = (propertyName, newValue) =>
        setOptions(o => ({ ...o, [propertyName]: newValue }));

    return (
        <Page>
            <Title text="Outstanding Works Orders Report" />
            {citsLoading ? (
                <Loading />
            ) : (
                <Grid container spacing={3}>
                    <Grid item xs={4}>
                        <Dropdown
                            items={filterOptions}
                            label="Report Type"
                            propertyName="reportType"
                            onChange={handleFieldChange}
                            value={options.reportType}
                        />
                    </Grid>
                    <Grid item xs={8} />
                    {options.reportType === 'CIT' && (
                        <Fragment>
                            <Grid item xs={4}>
                                <Dropdown
                                    label="CITs"
                                    propertyName="cit"
                                    items={citOptions}
                                    value={options.cit || ''}
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={8} />
                            <Grid item xs={12}>
                                <Button color="primary" variant="contained" onClick={handleClick}>
                                    Run Report
                                </Button>
                            </Grid>
                        </Fragment>
                    )}
                </Grid>
            )}
        </Page>
    );
}

export default OutstandingWorksOrdersReportOptions;
