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

// Report type option - ALL, Part Number, Cit
// on select that show and hide the dropdowns for those two

function OutstandingWorksOrdersReportOptions({ cits, citsLoading }) {
    const [options, setOptions] = useState({ reportType: 'All', partNumber: '', cit: '' });
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

    const filterOptions = ['All', 'Part Number', 'CIT'];

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
                    {options.reportType === 'Part Number' && (
                        <Fragment>
                            <Grid item xs={4}>
                                <InputField disabled value="FETCH" label="Partz" />
                            </Grid>
                            <Grid item xs={8} />
                        </Fragment>
                    )}
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
                        </Fragment>
                    )}
                </Grid>
            )}
        </Page>
    );
}

export default OutstandingWorksOrdersReportOptions;
