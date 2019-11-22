import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import AssemblyFailsDetailsOptions from '../../components/reports/AssemblyFailsDetailsOptions';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.assemblyFailsDetails.item);

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state)
});

export default connect(mapStateToProps, null)(withRouter(AssemblyFailsDetailsOptions));
