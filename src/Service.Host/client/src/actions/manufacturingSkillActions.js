import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { manufacturingSkillActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.manufacturingSkill.actionType,
    itemTypes.manufacturingSkill.uri,
    actionTypes,
    config.appRoot
);
