import { connect } from 'react-redux';
import { fetchErrorSelectors, ReportSelectors } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import actions from '../../actions/departmentActions';
import initialiseOnMount from '../initialiseOnMount';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import BuildsDetailReportOptions from '../../components/buildsbyDepartment/BuildsDetailReportOptions';

const reportSelectors = new ReportSelectors('buildsDetailReport');

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    departments: departmentsSelectors.getItems(state),
    departmentsLoading: departmentsSelectors.getLoading(state),
    errorMessage: fetchErrorSelectors(state),
    prevOptions: reportSelectors.getReportOptions(state),
    options: getOptions(ownProps)
});

const initialise = () => dispatch => {
    dispatch(actions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(BuildsDetailReportOptions));
