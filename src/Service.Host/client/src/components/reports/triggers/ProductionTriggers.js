import React, { Fragment, useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Loading,
    Title,
    Dropdown,
    InputField,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import moment from 'moment';
import { nodeInternals } from 'stack-utils';
import Page from '../../../containers/Page';
import TriggersList from './TriggersList';

function ProductionTriggers({
    reportData,
    loading,
    cits,
    options,
    fetchTriggers,
    history,
    itemError
}) {
    const [reportFormat, setReportFormat] = useState('BRIEF');
    const [citOptions, setCitOptions] = useState(['']);
    const [jobref, setJobref] = useState('');

    const citsFormatted = () => cits.filter(excludeInvalidCits).map(cit => ({
        id: cit.code,
        displayText: cit.name
    }));

    function excludeInvalidCits(cit) {
        return !cit.dateInvalid;
    }

   
    const handleLengthChange = (propertyName, newValue) => {
        setReportFormat(newValue);
    };

    const handleJobrefChange = (propertyName, newValue) => {
        setJobref(newValue);
        if (jobref.length === 6) {
            history.push(
                `/production/reports/triggers?citCode=${reportData.citCode}&jobref=${jobref}`
            );
            const newOptions = { citCode: reportData.citCode, jobref };
            fetchTriggers(newOptions);
        }
    };

    const handleCitChange = (propertyName, newValue) => {
        history.push(
            `/production/reports/triggers?citCode=${newValue}&jobref=${reportData.ptlJobref}`
        );
        const newOptions = { citCode: newValue, jobref: reportData.ptlJobref };
        fetchTriggers(newOptions);
    };

    function showInReport(trigger) {
        return reportFormat === 'FULL' || reportFormat === trigger.reportFormat;
    }

    function numOfParts(triggers) {
        const results = triggers.filter(showInReport);

        if (results.length === 1) {
            return '1 Part';
        }

        if (results.length > 1) {
            return `${results.length} Parts`;
        }

        return 'No Parts';
    }

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title text="Production Trigger Levels and Recommend Builds" />
                    {loading ? <Loading /> : ''}
                </Grid>
                {itemError ? (
                    <ErrorCard errorMessage={itemError.details?.message} />
                ) : (
                    <Grid item xs={12}>
                        {reportData ? (
                            <Fragment>
                                <Grid container>
                                    <Grid item xs={4}>
                                        <Dropdown
                                            label="CIT"
                                            propertyName="cit"
                                            items={citsFormatted()}
                                            value={reportData.citCode || ''}
                                            onChange={handleCitChange}
                                            allowNoValue
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <Dropdown
                                            label="Length"
                                            propertyName="reportFormat"
                                            items={[
                                                { id: 'BRIEF', displayText: 'Brief' },
                                                { id: 'FULL', displayText: 'Full' }
                                            ]}
                                            value={reportFormat}
                                            onChange={handleLengthChange}
                                            helperText={numOfParts(reportData.triggers)}
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <InputField
                                            fullWidth
                                            value={jobref}
                                            placeholder={reportData.ptlJobref}
                                            label="Jobref"
                                            maxLength={6}
                                            onChange={handleJobrefChange}
                                            helperText={
                                                reportData.ptlRunDateTime
                                                    ? `Last run ${moment(
                                                          reportData.ptlRunDateTime
                                                      ).format('DD-MMM HH:mm')}`
                                                    : ''
                                            }
                                        />
                                    </Grid>
                                </Grid>
                                <TriggersList
                                    triggers={reportData.triggers.filter(showInReport)}
                                    jobref={reportData.ptlJobref}
                                    citcode={reportData.citCode}
                                />
                            </Fragment>
                        ) : (
                            ''
                        )}
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

ProductionTriggers.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    config: PropTypes.shape({})
};

ProductionTriggers.defaultProps = {
    reportData: null,
    config: null,
    loading: false
};

export default ProductionTriggers;
