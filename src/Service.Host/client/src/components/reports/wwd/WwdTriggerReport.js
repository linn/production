import React, { Fragment } from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../../containers/Page';
import WwdDetailsTable from './WwdDetailsTable';

function WwdTriggerReport({ reportData, loading, itemError }) {
    return (
        <Page>
            <Grid container spacing={3} justify="center">
                {loading ? (
                    <Grid item xs={12}>
                        <Title text="What Will Decrement From A Workstation" />
                        <Loading />
                    </Grid>
                ) : (
                    ''
                )}
                {itemError ? (
                    <ErrorCard errorMessage={itemError.details?.message} />
                ) : (
                    <Grid item xs={12}>
                        {reportData ? (
                            <Fragment>
                                <Title text="What Will Decrement From A Workstation" />
                                <span>{`${reportData.qty} x ${reportData.partNumber} from workstation ${reportData.workStationCode}`}</span>
                                <WwdDetailsTable details={reportData.wwdDetails} />
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

WwdTriggerReport.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    itemError: PropTypes.shape({})
};

WwdTriggerReport.defaultProps = {
    reportData: null,
    loading: false,
    itemError: null
};

export default WwdTriggerReport;
