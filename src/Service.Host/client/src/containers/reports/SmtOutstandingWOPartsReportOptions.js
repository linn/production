import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import SmtOutstandingWOPartsReportOptions from '../../components/reports/SmtOutstandingWOPartsReportOptions';

const reportSelectors = new ReportSelectors('smtOutstandingWorkOrderParts');

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state)
});

export default connect(
    mapStateToProps,
    null
)(withRouter(SmtOutstandingWOPartsReportOptions));
