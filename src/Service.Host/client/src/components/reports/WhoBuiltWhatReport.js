import React from 'react';
import { Loading, Title } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';
import MultiReportTable from '../MultiReportTable';

const WhoBuiltWhat = ({ reportData, loading, options }) => {
    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title
                        text={`Who built what between ${moment(options.fromDate).format(
                            'DD-MMM-YYYY'
                        )} and ${moment(options.toDate).format('DD-MMM-YYYY')} `}
                    />
                </Grid>
                <Grid item xs={6}>
                    {loading || !reportData ? (
                        <Loading />
                    ) : (
                        <div>
                            <MultiReportTable
                                reportData={reportData}
                                showTotals={false}
                                placeholderRows={10}
                                placeholderColumns={3}
                                showRowTitles
                                showTitle
                            />
                        </div>
                    )}
                </Grid>
                <Grid item xs={6} />
            </Grid>
        </Page>
    );
};

WhoBuiltWhat.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

WhoBuiltWhat.defaultProps = {
    reportData: null,
    options: {},
    loading: false
};

export default WhoBuiltWhat;
