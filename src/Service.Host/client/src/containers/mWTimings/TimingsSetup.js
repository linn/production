import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import TimingsSetup from '../../components/mWTimings/TimingsSetup';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.metalWorkTimingsReport.item);

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state)
});

export default connect(mapStateToProps, null)(withRouter(TimingsSetup));
