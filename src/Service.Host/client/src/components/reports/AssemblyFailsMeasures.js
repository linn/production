import React from 'react';
import { Loading, ReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Box from '@material-ui/core/Box';
import Page from '../../containers/Page';

const AssemblyFailsMeasures = ({ reportData, loading, options }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                <Box paddingBottom={3}>
                    {loading || !reportData ? (
                        <Loading />
                    ) : (
                        <ReportTable
                            reportData={reportData}
                            title={reportData.title}
                            showTitle
                            showTotals
                            placeholderRows={4}
                            placeholderColumns={4}
                            showRowTitles
                        />
                    )}
                </Box>
            </Grid>
        </Grid>
    </Page>
);

AssemblyFailsMeasures.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

AssemblyFailsMeasures.defaultProps = {
    reportData: [],
    options: {},
    loading: false
};

export default AssemblyFailsMeasures;
