import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ViewManufacturingSkills from '../../components/manufacturingSkills/ManufacturingSkills';
import manufacturingSkillsActions from '../../actions/manufacturingSkillsActions';
import manufacturingSkillsSelectors from '../../selectors/manufacturingSkillsSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    items: manufacturingSkillsSelectors.getItems(state),
    loading: manufacturingSkillsSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.manufacturingSkills.item)
});

const initialise = () => dispatch => {
    dispatch(manufacturingSkillsActions.searchWithOptions('', '&includeInvalid=false'));
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ViewManufacturingSkills));
