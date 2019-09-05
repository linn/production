import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../../containers/Page';
import ReportHeader from './ReportHeader';
import TriggersList from './TriggersList';

function ProductionTriggers({ reportData, loading }) {
    // <Title text="Production Trigger Levels and Recommend Builds" />

    /*
                <Grid item xs={12}>
                    {loading ? <Loading /> : ''}
                </Grid>
                {reportData ? (
                    <Grid item xs={12}>
                        <ReportHeader citCode={reportData.citCode} citName={reportData.citName} />                                        
                    </Grid>
                ) : (
                    <Loading />
                )}
*/

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title text="Production Trigger Levels and Recommend Builds" />
                </Grid>
                <Grid item xs={12}>
                    {loading ? <Loading /> : ''}
                    {reportData ? <ReportHeader citName={reportData.citName} /> : ''}
                    {reportData ? (
                        <TriggersList
                            triggers={reportData.triggers}
                            jobref={reportData.ptlJobref}
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
