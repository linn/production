import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import BoardTestsReportOptions from '../../components/reports/BoardTestsReportOptions';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.boardTestsReport.item);

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state)
});

const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(BoardTestsReportOptions));
