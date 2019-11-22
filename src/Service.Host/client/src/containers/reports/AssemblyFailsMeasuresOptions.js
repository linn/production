import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import AssemblyFailsMeasuresOptions from '../../components/reports/AssemblyFailsMeasuresOptions';

const reportSelectors = new ReportSelectors('assemblyFailsMeasures');

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state)
});

export default connect(mapStateToProps, null)(withRouter(AssemblyFailsMeasuresOptions));
