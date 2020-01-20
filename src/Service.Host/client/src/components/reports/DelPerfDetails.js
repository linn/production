import React from 'react';
import { Loading, ReportTable, BackButton } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const handleBackClick = (history, options) => {
    history.push(`/production/reports/delperf?citCode=${options.citCode}`);
};

const DelPerfDetails = ({ reportData, loading, history, options }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                {loading || !reportData ? (
                    <Loading />
                ) : (
                    <ReportTable
                        reportData={reportData}
                        title={reportData.title}
                        showTitle
                        showTotals={false}
                    />
                )}
            </Grid>
            <Grid item xs={12}>
                <BackButton backClick={() => handleBackClick(history, options)} />
            </Grid>
        </Grid>
    </Page>
);

DelPerfDetails.propTypes = {
    reportData: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

DelPerfDetails.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default DelPerfDetails;
