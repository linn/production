import { connect } from 'react-redux';
import { fetchErrorSelectors } from '@linn-it/linn-form-components-library';
import actions from '../../actions/departmentActions';
import initialiseOnMount from '../initialiseOnMount';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import BuildsDetailReportOptions from '../../components/buildsbyDepartment/BuildsDetailReportOptions';

const mapStateToProps = state => ({
    departments: departmentsSelectors.getItems(state),
    departmentsLoading: departmentsSelectors.getLoading(state),
    errorMessage: fetchErrorSelectors(state)
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
