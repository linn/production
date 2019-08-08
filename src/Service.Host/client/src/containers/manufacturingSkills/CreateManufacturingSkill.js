import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingSkill from '../../components/manufacturingSkills/ManufacturingSkill';
import manufacturingSkillActions from '../../actions/manufacturingSkillActions';
import manufacturingSkillSelectors from '../../selectors/manufacturingSkillSelectors';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    errorMessage: fetchErrorSelectors(state),
    loading: manufacturingSkillSelectors.getLoading(state),
    snackbarVisible: manufacturingSkillSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(manufacturingSkillActions.setEditStatus('create'));
    dispatch(manufacturingSkillActions.create());
};

const mapDispatchToProps = {
    initialise,
    addItem: manufacturingSkillActions.add,
    setEditStatus: manufacturingSkillActions.setEditStatus,
    setSnackbarVisible: manufacturingSkillActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ManufacturingSkill));
