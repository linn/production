import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import AteStatusReportOptions from '../../components/reports/AteStatusReportOptions';

const reportSelectors = new ReportSelectors('ateStatusReport');

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state),
    byDate: true
});

export default connect(mapStateToProps, null)(withRouter(AteStatusReportOptions));
