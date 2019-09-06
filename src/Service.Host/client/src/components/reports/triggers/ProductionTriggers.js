import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Dropdown } from '@linn-it/linn-form-components-library';
import { Loading, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../../containers/Page';
import TriggersList from './TriggersList';

function ProductionTriggers({ reportData, loading, cits, options }) {
    const [reportFormat, setReportFormat] = useState('BRIEF');
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

    const handleLengthChange = (propertyName, newValue) => {
        setReportFormat(newValue);
    };

    const handleCitChange = (propertyName, newValue) => {
        options.citCode = newValue;
    };

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title text="Production Trigger Levels and Recommend Builds" />
                    {loading ? <Loading /> : ''}
                </Grid>
                <Grid item xs={12}>
                    {reportData ? (
                        <Grid container>
                            <Grid item xs={4}>
                                <Dropdown
                                    label="CIT"
                                    propertyName="cit"
                                    items={citOptions}
                                    value={options.citCode || ''}
                                    onChange={handleCitChange}
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
                                />
                            </Grid>
                        </Grid>
                    ) : (
                        ''
                    )}
                    {reportData ? (
                        <TriggersList
                            triggers={reportData.triggers}
                            jobref={reportData.ptlJobref}
                            reportFormat={reportFormat}
                        />
                    ) : (
                        ''
                    )}
                </Grid>
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
